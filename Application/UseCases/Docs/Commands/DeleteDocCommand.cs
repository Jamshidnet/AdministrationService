using Application.Common.Abstraction;
using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Commands;


public record DeleteDocCommand(Guid DocId) : IRequest;
public class DeleteDocCommandHandler : IRequestHandler<DeleteDocCommand>
{
    private readonly IApplicationDbContext _context;

    public IDocChangeLogger _logger { get; set; }
    public DeleteDocCommandHandler(IApplicationDbContext context, IDocChangeLogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(DeleteDocCommand request, CancellationToken cancellationToken)
    {
        var doc = await _context.Docs.FindAsync(request.DocId, cancellationToken)
            ?? throw new NotFoundException(nameof(Docs), request.DocId);
        _ = _context.Docs.Remove(doc);

        await _logger.Log(doc.Id, "Delete");

        var result = await _context.SaveChangesAsync(cancellationToken);


    }
}
