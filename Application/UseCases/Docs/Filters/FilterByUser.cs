using Application.UseCases.Docs.Responses;
using AutoMapper;
using MediatR;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Filters;

public class FilterByUser : IRequest<List<FilterByUserResponse>>
{
    public Guid? RegionId { get; set; }
    public bool ByRegion { get; set; } = false;
    public Guid? DistrictId { get; set; }
    public bool ByDistrict { get; set; } = false;
    public Guid? QuarterId { get; set; }
    public bool ByQuarter { get; set; } = false;

}
public class FilterByUserHandler : IRequestHandler<FilterByUser, List<FilterByUserResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public FilterByUserHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;

    /// <summary>
    /// Here Handler function executes a function in database. 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<FilterByUserResponse>> Handle(FilterByUser request, CancellationToken cancellationToken)
    {


        
        var result = _dbContext.GetFilteredUsers(
                                                 request.RegionId,
                                                 request.DistrictId,
                                                 request.QuarterId,
                                                 request.ByRegion,
                                                 request.ByDistrict,
                                                 request.ByQuarter
                                                 );

        return Task.FromResult(result.ToList());
    }
}

