using Application.Common.Exceptions;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Districts.Commands;


public record DeleteDistrictCommand(Guid DistrictId) : IRequest;
public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteDistrictCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        var district = await _context.Districts.FindAsync(request.DistrictId, cancellationToken);
        if (district is null)
        {
            throw new NotFoundException(nameof(Districts), request.DistrictId);
        }
         _context.Districts.Remove(district);
        var result = await _context.SaveChangesAsync(cancellationToken);
    }
}
