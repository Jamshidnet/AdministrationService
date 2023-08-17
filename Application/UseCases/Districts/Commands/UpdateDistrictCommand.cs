using Application.Common.Exceptions;
using Application.UseCases.Districts.Responses;
using AutoMapper;
using Domein.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Districts.Commands;

public class UpdateDistrictCommand : IRequest<DistrictResponse>
{
    public Guid Id { get; set; }

    public string DistrictName { get; set; }

    public Guid RegionId { get; set; }

    public ICollection<Guid> Quarters { get; set; }
}
public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, DistrictResponse>
{
    private IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdateDistrictCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<DistrictResponse> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        var quarters = await _context.Quarters.ToListAsync(cancellationToken);
        var foundDistrict = await _context.Districts.FindAsync(new object[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(District), request.Id);

        if (request?.Quarters?.Count >= 0)
        {
            foundDistrict?.Quarters?.Clear();
            quarters.ForEach(quarter =>
            {
                if (request.Quarters.Any(id => id == quarter.Id))
                    foundDistrict.Quarters.Add(quarter);
            });

        }
        foundDistrict.RegionId = request.RegionId;
        foundDistrict.DistrictName = request.DistrictName;
        _ = _context.Districts.Update(foundDistrict);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<DistrictResponse>(foundDistrict);
    }
}
