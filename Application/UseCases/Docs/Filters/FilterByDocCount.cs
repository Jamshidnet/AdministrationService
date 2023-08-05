using Application.UseCases.Categories.Responses;
using Application.UseCases.Districts.Responses;
using Application.UseCases.Quarters.Responses;
using Application.UseCases.Regions.Responses;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewProject.Abstraction;

namespace Application.UseCases.Docs.Filters;

public record FilterByDocCount : IRequest<List<DocCountResponse>>
{
    public Guid? RegionId { get; set; } 


    public bool ByRegion;

    public Guid? DistrictId { get; set; } 


    public bool ByDistrict;

    public Guid? QuarterId { get; set; } 


    public bool ByQuarter;

    public Guid? CategoryId { get; set; }

    public bool ByCategory; 
}


public class FilterByDocCountHandler : IRequestHandler<FilterByDocCount, List<DocCountResponse>>
{
    IApplicationDbContext _dbContext;
    IMapper _mapper;

    public FilterByDocCountHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<DocCountResponse>> Handle(FilterByDocCount request, CancellationToken cancellationToken)
    {

        var docs = await _dbContext.Docs.ToListAsync(cancellationToken);

        if (request.RegionId is not null)
            docs = docs.Where(x => x.Client.Person.Quarter.District.RegionId == request.RegionId).ToList();

        if (request.DistrictId is not null)
            docs = docs.Where(x => x.Client.Person.Quarter.DistrictId == request.DistrictId).ToList();
        
        if (request.QuarterId is not null)
            docs = docs.Where(x => x.Client.Person.QuarterId == request.DistrictId).ToList();
       
        if (request.CategoryId is not null)
            docs = docs.Where(x => x.ClientAnswers
                           .DistinctBy(x => x.QuestionId)
                           .Any(ans => ans.Question.CategoryId == request.CategoryId)).ToList();



        if (request.ByRegion)
        {
            var list = from region in await _dbContext.Regions.ToListAsync(cancellationToken)
                       select new DocCountResponse()
                       {
                           Region = _mapper.Map<RegionResponse>(region),
                           DocumentCount = docs.Where(d => d.Client.Person.Quarter.District.RegionId == region.Id).Count()
                       };

            return list.ToList();
        }

        if(request.ByDistrict)
        {
            var list = from district in await _dbContext.Districts.ToListAsync(cancellationToken)
                       select new DocCountResponse()
                       {
                           District = _mapper.Map<DistrictResponse>(district),
                           DocumentCount = docs.Where(d => d.Client.Person.Quarter.DistrictId == district.Id).Count()
                       };

            return list.ToList();
        }

        if(request.ByQuarter)
        {
            var list = from quarter in await _dbContext.Quarters.ToListAsync(cancellationToken)
                       select new DocCountResponse()
                       {
                           Quarter = _mapper.Map<QuarterResponse>(quarter),
                           DocumentCount = docs.Where(d => d.Client.Person.QuarterId == quarter.Id).Count()
                       };

            return list.ToList();
        }

        if(request.ByCategory)
        {
            var list = from category in await _dbContext.Categories.ToListAsync(cancellationToken)
                       select new DocCountResponse()
                       {
                           Category = _mapper.Map<CategoryResponse>(category),
                           DocumentCount = docs.Where(x=>x.ClientAnswers
                           .DistinctBy(x=>x.QuestionId).Any(ans=>ans.Question.CategoryId==category.Id)).Count()
                       };

            return list.ToList();
        }

        return _mapper.Map<List<DocCountResponse>>(docs);
    }
}
