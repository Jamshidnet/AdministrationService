
using Application.UseCases.Questions.Responses;

namespace Application.UseCases.DefaultAnswers.Responses;

public class DefaultAnswerResponse
{
    public Guid Id { get; set; }

    public GetListQuestionResponse Question { get; set; }

    public string AnswerText { get; set; }
}
