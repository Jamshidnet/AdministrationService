using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Quarters.Commands;


public record DeleteQuarterCommand(Guid QuarterId) : IRequest;
public class DeleteQuarterCommandHandler : IRequestHandler<DeleteQuarterCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteQuarterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteQuarterCommand request, CancellationToken cancellationToken)
    {
        var quarter = await _context.Quarters.FindAsync(request.QuarterId, cancellationToken);
        if (quarter is null)
        {
            throw new NotFoundException(nameof(Quarters), request.QuarterId);
        }
         _context.Quarters.Remove(quarter);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
