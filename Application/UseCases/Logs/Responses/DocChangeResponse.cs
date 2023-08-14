namespace Application.UseCases.Logs.Responses;

public class DocChangeResponse
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TableId { get; set; }

    public Guid? DocId { get; set; }

    public DateOnly? DateAt { get; set; }

    public string ActionName { get; set; }


}
