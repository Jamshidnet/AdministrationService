using Application.Common.Exceptions;
using AutoMapper;
using Domein.Entities;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Quarters.Commands;


public record CreateQuarterCommand(string QuarterName, Guid DistrictId) : IRequest<Guid>;

public class CreateQuarterCommandHandler : IRequestHandler<CreateQuarterCommand, Guid>
{

    private IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateQuarterCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateQuarterCommand request, CancellationToken cancellationToken)
    {
        await FilterIfDistrictExsists(request.DistrictId);
        Quarter quarter = _mapper.Map<Quarter>(request);
        quarter.Id = Guid.NewGuid();
        await _dbContext.Quarters.AddAsync(quarter);
        await _dbContext.SaveChangesAsync();
        return quarter.Id;
    }

    private async Task FilterIfDistrictExsists(Guid districtId)
    {
        if (await _dbContext.Districts.FindAsync(districtId) is null)
            throw new NotFoundException("There is no district with given Id. ");
    }

}

