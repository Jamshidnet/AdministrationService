using Application.UseCases.Categories.Responses;
using Application.UseCases.Districts.Responses;
using Application.UseCases.Quarters.Responses;
using Application.UseCases.Regions.Responses;


namespace Application.UseCases.Docs.Filters;

public class DocCountResponse
{
    public GetListRegionResponse Region { get; set; }

    public GetListDIstrictResponse District { get; set; }

    public GetListQuarterResponse Quarter { get; set; }

    public GetListCategoryResponse Category { get; set; }

    public int DocumentCount { get; set; }
}
