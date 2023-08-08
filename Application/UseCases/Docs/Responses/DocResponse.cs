using Application.UseCases.Clients.Responses;
using Application.UseCases.DefaultAnswers.Responses;

namespace Application.UseCases.Docs.Responses;

public class DocResponse
{
    public Guid Id { get; set; }

    public GetListClientResponse Client { get; set; }

    public string UserName { get; set; }

    public DateOnly? TakenDate { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string Device { get; set; }

    public virtual ICollection<ClientAnswerResponse> ClientAnswers { get; set; }

}
