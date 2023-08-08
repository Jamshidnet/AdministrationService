using Application.UseCases.Clients.Responses;
using Application.UseCases.Districts.Responses;


namespace Application.UseCases.Quarters.Responses;

public  class QuarterResponse
{
    public Guid Id { get; set; }

    public string QuarterName { get; set; }

    public GetListDIstrictResponse District { get; set; }

    public virtual ICollection<GetListClientResponse> Clients { get; set; }


}
