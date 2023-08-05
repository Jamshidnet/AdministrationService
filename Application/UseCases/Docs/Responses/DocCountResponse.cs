using Application.UseCases.Categories.Responses;
using Application.UseCases.Districts.Responses;
using Application.UseCases.Quarters.Responses;
using Application.UseCases.Regions.Responses;


namespace Application.UseCases.Docs.Filters;

public class DocCountResponse
{
    public RegionResponse? Region { get; set; }

    public DistrictResponse? District { get; set; }

    public QuarterResponse? Quarter { get; set; }

    public CategoryResponse? Category { get; set; }

    public int DocumentCount { get; set; }
}
