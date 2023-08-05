using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Commands;


public record DeleteDocCommand(Guid DocId) : IRequest;
public class DeleteDocCommandHandler : IRequestHandler<DeleteDocCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDocCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDocCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Docs.FindAsync(request.DocId, cancellationToken);

        if (doc is null)
        {
            throw new NotFoundException(nameof(Docs), request.DocId);
        }
        _context.Docs.Remove(doc);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
