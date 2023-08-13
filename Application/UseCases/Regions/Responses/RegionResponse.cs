using Application.UseCases.Districts.Responses;

namespace Application.UseCases.Regions.Responses;

public class RegionResponse
{
    public Guid Id { get; set; }

    public string RegionName { get; set; }

    public virtual ICollection<GetListDIstrictResponse> Districts { get; set; }
}
