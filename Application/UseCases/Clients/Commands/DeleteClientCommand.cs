using Application.Common.Exceptions;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Clients.Commands;


public record DeleteClientCommand(Guid ClientId) : IRequest;
public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.FindAsync(request.ClientId, cancellationToken)
            ?? throw new NotFoundException(nameof(Clients), request.ClientId);
        _ = _context.Clients.Remove(client);

        var person = await _context.People.FindAsync(client.PersonId)
            ?? throw new NotFoundException(nameof(Person), client.PersonId);
        _ = _context.People.Remove(person);

        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
