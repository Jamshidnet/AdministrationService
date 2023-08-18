using Application.Common.Exceptions;
using Application.UseCases.Regions.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Regions.Commands;


public class UpdateRegionCommand : IRequest<RegionResponse>
{
    public Guid Id { get; set; }

    public string RegionName { get; set; }

    public ICollection<Guid> Districts { get; set; }
}
public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, RegionResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateRegionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<RegionResponse> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        var districts = await _context.Districts.ToListAsync(cancellationToken);
        var foundRegion = await _context.Regions.FindAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Region), request.Id);

        if (request?.Districts?.Count > 0)
        {
            foundRegion?.Districts?.Clear();
            districts.ForEach(role =>
            {
                if (request.Districts.Any(id => id == role.Id))
                    foundRegion.Districts.Add(role);
            });

        }

        foundRegion.RegionName = request.RegionName;
         _context.Regions.Update(foundRegion);
         await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<RegionResponse>(foundRegion);
    }
}
