using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Clients.Commands;


public class CreateClientCommand : IRequest<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public Guid QuarterId { get; set; }

    public Guid ClientTypeId { get; set; }


}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateClientCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        Person person = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthdate = DateOnly.FromDateTime(request.Birthdate),
            PhoneNumber = request.PhoneNumber,
            QuarterId = request.QuarterId,
            Id = Guid.NewGuid()
        };
         await _dbContext.People.AddAsync(person);

        Client client = new()
        {
            PersonId = person.Id,
            ClientTypeId = request.ClientTypeId,
            Id = Guid.NewGuid()
        };
         await _dbContext.Clients.AddAsync(client);
         await _dbContext.SaveChangesAsync();
        return client.Id;
    }
}

