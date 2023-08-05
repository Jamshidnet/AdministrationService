using Application.UseCases.Questions.Responses;

namespace Application.UseCases.QuestionTypes.Responses;

public class QuestionTypeResponse
{
    public Guid Id { get; set; }

    public string QuestionTypeName { get; set; }

    public virtual ICollection<QuestionResponse> Questions { get; set; }
}
