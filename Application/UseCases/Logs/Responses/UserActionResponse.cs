namespace Application.UseCases.Logs.Responses;

public class UserActionResponse
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string ActionName { get; set; }

    public Guid? TableId { get; set; }

    public DateTime? ActionTime { get; set; }
}
