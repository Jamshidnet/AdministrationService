using Application.Common.Abstraction;
using Application.Common.Exceptions;
using Application.UseCases.Clients.Commands;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Commands;


public record CreateDocCommand : IRequest<Guid>
{
    public CreateClientCommand client { get; set; }

    public Guid? ClientId { get; set; }
    public ClientInDocResponse[] ClientAnswers { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string Device { get; set; }
}

public class CreateDocCommandHandler : IRequestHandler<CreateDocCommand, Guid>
{

    private IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;

    private ICurrentUserService _currentUser;
    public IDocChangeLogger _logger { get; set; }

    public CreateDocCommandHandler(
        IApplicationDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUser,
        IDocChangeLogger logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateDocCommand request, CancellationToken cancellationToken)
    {
        Doc doc = _mapper.Map<Doc>(request);
        doc.Id = Guid.NewGuid();
        doc.TakenDate = DateOnly.FromDateTime(DateTime.Now);
        
        if (request.ClientId is null)
        {
            Person person = new()
            {
                FirstName = request.client.FirstName,
                LastName = request.client.LastName,
                Birthdate = DateOnly.FromDateTime(request.client.Birthdate),
                PhoneNumber = request.client.PhoneNumber,
                QuarterId = request.client.QuarterId,
                Id = Guid.NewGuid()
            };

            Client client = new()
            {
                Id = Guid.NewGuid(),
                PersonId = person.Id,
                ClientTypeId = request.client.ClientTypeId,
            };

            doc.Client = client;
            _ = await _dbContext.Clients.AddAsync(client);
            _ = await _dbContext.People.AddAsync(person);
        }
        else
        {
            doc.Client = await FilterIfClienExsists(request.ClientId);
        }
        var clientAnswers = _mapper.Map<ClientAnswer[]>(request.ClientAnswers).ToList();
        clientAnswers.ForEach(x =>
        {
            x.Id = Guid.NewGuid();
            x.DocId = doc.Id;
        });


        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == _currentUser.Username)
            ?? throw new NotFoundException(
                " a user with the username that exists within the incoming claim couldn't found in database. ");




        doc.ClientAnswers = clientAnswers;

        doc.UserId = user.Id;
        _ = await _dbContext.Docs.AddAsync(doc);
        await _dbContext.ClientAnswers.AddRangeAsync(clientAnswers);
        _ = await _dbContext.SaveChangesAsync();

        await _logger.Log(doc.Id, "Create");

        return doc.Id;

    }

    private async Task<Client> FilterIfClienExsists(Guid? clientId)
    {
        return await _dbContext.Clients.FindAsync(clientId)
             ?? throw new NotFoundException("There is no client with given Id. ");
    }

}

public record ClientInDocResponse(string AnswerText, Guid QuestionId, Guid? DefaultAnswerId);
