using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Regions.Commands;

public record DeleteRegionCommand(Guid RegionId) : IRequest;
public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRegionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        var region = await _context.Regions.FindAsync(request.RegionId, cancellationToken);
        if (region is null)
        {
            throw new NotFoundException(nameof(Regions), request.RegionId);
        }
        _ = _context.Regions.Remove(region);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
