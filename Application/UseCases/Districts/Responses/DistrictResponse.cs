using Application.UseCases.Quarters.Responses;
using Domein.Entities;

namespace Application.UseCases.Districts.Responses;

public  class DistrictResponse
{
    public Guid Id { get; set; }

    public string DistrictName { get; set; }


    public Guid RegionId { get; set; }

    public virtual ICollection<QuarterResponse> Quarters { get; set; }

}
