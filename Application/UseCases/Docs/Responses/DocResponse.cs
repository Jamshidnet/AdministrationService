using Application.UseCases.DefaultAnswers.Responses;

namespace Application.UseCases.Docs.Responses;

public class DocResponse
{
    public Guid Id { get; set; }

    public string ClientName { get; set; }

    public string UserName { get; set; }

    public DateOnly? TakenDate { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string Device { get; set; }

    public virtual ICollection<DefaultAnswerResponse> DefaultAnswers { get; set; }

}
