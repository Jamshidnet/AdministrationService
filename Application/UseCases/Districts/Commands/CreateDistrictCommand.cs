using Application.Common.Exceptions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Districts.Commands;
public record CreateDistrictCommand(string DistrictName, Guid RegionId) : IRequest<Guid>;

public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, Guid>
{
    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateDistrictCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
    {
        await FilterIfRegionExsists(request.RegionId);
        District district = _mapper.Map<District>(request);
        district.Id = Guid.NewGuid();
        _ = await _dbContext.Districts.AddAsync(district);
        _ = await _dbContext.SaveChangesAsync();
        return district.Id;

    }
    private async Task FilterIfRegionExsists(Guid regionId)
    {
        if (await _dbContext.Regions.FindAsync(regionId) is null)
            throw new NotFoundException("There is no region with given Id. ");
    }


}
