using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Regions.Commands;

public record CreateRegionCommand(string RegionName) : IRequest<Guid>;

public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateRegionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        Region region = _mapper.Map<Region>(request);
        region.Id = Guid.NewGuid();
        await _dbContext.Regions.AddAsync(region);
        await _dbContext.SaveChangesAsync();
        return region.Id;
    }
}
