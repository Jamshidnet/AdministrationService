using Application.UseCases.Clients.Responses;
using Domein.Entities;

namespace Application.UseCases.Quarters.Responses;

public  class QuarterResponse
{
    public Guid Id { get; set; }

    public string QuarterName { get; set; }

    public Guid? DistrictId { get; set; }

    public virtual ICollection<ClientResponse> Clients { get; set; }


}
