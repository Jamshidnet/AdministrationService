using Application.Common.Exceptions;
using Application.UseCases.Clients.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Clients.Commands;

public class UpdateClientCommand : IRequest<ClientResponse>
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public string PhoneNumber { get; set; }

    public Guid QuarterId { get; set; }

    public Guid ClientTypeId { get; set; }
}
public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateClientCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<ClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
       await  FilterIfQuarterExsists(request.QuarterId);
        var foundClient = await _context.Clients.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(Client), request.Id);
        Person person = await FilterIfPersonExsists(foundClient.PersonId);
        person.FirstName = request.FirstName;
        person.LastName = request.LastName;
        person.Birthdate = DateOnly.FromDateTime(request.Birthdate);
        person.PhoneNumber = request.PhoneNumber;
        person.QuarterId = request.QuarterId;
         _context.People.Update(person);

        foundClient.ClientTypeId = request.ClientTypeId;
       
        _context.Clients.Update(foundClient);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ClientResponse>(foundClient);
    }

    private async Task<Person> FilterIfPersonExsists(Guid personId)
    {
        return await _context.People.FindAsync(personId)
            ?? throw new NotFoundException(
                " there is no person with the id which client object consists. ");
    }

    private async Task FilterIfQuarterExsists(Guid quarterId)
    {
        if (await _context.Quarters.FindAsync(quarterId) is null)
            throw new NotFoundException("There is no quarter with given Id. ");
    } 


}
