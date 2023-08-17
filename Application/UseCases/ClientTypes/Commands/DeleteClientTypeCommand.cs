using Application.Common.Exceptions;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.ClientTypes.Commands;



public record DeleteClientTypeCommand(Guid ClientTypeId) : IRequest;
public class DeleteClientTypeCommandHandler : IRequestHandler<DeleteClientTypeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientTypeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteClientTypeCommand request, CancellationToken cancellationToken)
    {
        var clientType = await _context.ClientTypes.FindAsync(request.ClientTypeId, cancellationToken);
        if (clientType is null)
        {
            throw new NotFoundException(nameof(ClientType), request.ClientTypeId);
        }
        _ = _context.ClientTypes.Remove(clientType);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
