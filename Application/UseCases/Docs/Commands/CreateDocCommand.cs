using Application.Common.Abstraction;
using Application.Common.Exceptions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Commands;


public record CreateDocCommand : IRequest<Guid>
{
    public Guid ClientId { get; set; }
    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string Device { get; set; }
}

public class CreateDocCommandHandler : IRequestHandler<CreateDocCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private ICurrentUserService _currentUser;
    public CreateDocCommandHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateDocCommand request, CancellationToken cancellationToken)
    {

        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == _currentUser.Username)
            ?? throw new NotFoundException(
                " a user with the username that exists within the incoming claim couldn't found in database. ");

        await FilterIfClienExsists(request.ClientId);

        Doc doc = _mapper.Map<Doc>(request);


        doc.Id = Guid.NewGuid();
            doc.TakenDate = DateOnly.FromDateTime(DateTime.Now);
            doc.UserId = user.Id;
        await _dbContext.Docs.AddAsync(doc);
        await _dbContext.SaveChangesAsync();
        return doc.Id;
    }

    private async Task FilterIfClienExsists(Guid clientId)
    {
        if (await _dbContext.Clients.FindAsync(clientId) is null)
            throw new NotFoundException("There is no client with given Id. ");
    }

}
