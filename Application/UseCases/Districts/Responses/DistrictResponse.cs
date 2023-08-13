using Application.UseCases.Quarters.Responses;
using Application.UseCases.Regions.Responses;

namespace Application.UseCases.Districts.Responses;

public class DistrictResponse
{
    public Guid Id { get; set; }

    public string DistrictName { get; set; }


    public GetListRegionResponse Region { get; set; }

    public virtual ICollection<GetListQuarterResponse> Quarters { get; set; }

}
