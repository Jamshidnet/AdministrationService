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
    public bool ByRegion { get; set; } = false; 
    public Guid? DistrictId { get; set; }
    public bool ByDistrict { get; set; } = false;
    public Guid? QuarterId { get; set; }
    public bool ByQuarter { get; set; } = false;
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

        var categories = await _dbContext.Categories.ToListAsync(cancellationToken);

        if (request.ByRegion)
        {

            if (request.RegionId is not null)
                docs = docs.Where(x => x.Client.Person.Quarter.District.RegionId == request.RegionId).ToList();

            var regions = await _dbContext.Regions.ToListAsync(cancellationToken);

            var list = new List<DocCountResponse>();
             regions.ForEach(region =>
             list.AddRange( from category in categories
                               select new DocCountResponse()
                               {
                                   Region = _mapper.Map<GetListRegionResponse>(region),
                                   Category=_mapper.Map<GetListCategoryResponse>(category),
                                   DocumentCount = docs.Where(d => d.Client.Person.Quarter.District.RegionId == region.Id
                                   && d.ClientAnswers.DistinctBy(x=>x.QuestionId).Any(y=>y.Question.CategoryId==category.Id)).Count()
                               }));
            

            return list.ToList();
        }
        else if(request.ByDistrict)
        {
            if (request.DistrictId is not null)
                docs = docs.Where(x => x.Client.Person.Quarter.DistrictId == request.DistrictId).ToList();

            var districts = await _dbContext.Districts.ToListAsync(cancellationToken);
           
            var list = new List<DocCountResponse>();
            districts.ForEach(district =>
            list.AddRange(from category in categories
                          select new DocCountResponse()
                          {
                              Region = _mapper.Map<GetListRegionResponse>(district.Region),
                              District= _mapper.Map<GetListDIstrictResponse>(district),
                              Category = _mapper.Map<GetListCategoryResponse>(category),
                              DocumentCount = docs.Where(d => d.Client.Person.Quarter.DistrictId == district.Id
                               && d.ClientAnswers.DistinctBy(x => x.QuestionId).Any(y => y.Question.CategoryId == category.Id)).Count()
                          }));


            return list.ToList();
        }
        else
        {
            if (request.QuarterId is not null)
                docs = docs.Where(x => x.Client.Person.QuarterId == request.DistrictId).ToList();

            var quarters = await _dbContext.Quarters.ToListAsync(cancellationToken);
            var list = new List<DocCountResponse>();
            quarters.ForEach(quarter =>
            list.AddRange(from category in categories
                          select new DocCountResponse()
                          {
                              Region = _mapper.Map<GetListRegionResponse>(quarter.District.Region),
                              District = _mapper.Map<GetListDIstrictResponse>(quarter.District),
                              Quarter = _mapper.Map<GetListQuarterResponse>(quarter),
                              Category = _mapper.Map<GetListCategoryResponse>(category),
                              DocumentCount = docs.Where(d => d.Client.Person.QuarterId == quarter.Id
                               && d.ClientAnswers.DistinctBy(x => x.QuestionId).Any(y => y.Question.CategoryId == category.Id)).Count()
                          }));


            return list.ToList();
        }
    }
}
