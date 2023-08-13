using Application.UseCases.Categories.Responses;
using Application.UseCases.DefaultAnswers.Responses;
using Application.UseCases.QuestionTypes.Responses;

namespace Application.UseCases.Questions.Responses;

public class QuestionResponse
{
    public Guid Id { get; set; }

    public string QuestionText { get; set; }

    public GetListCategoryResponse Category { get; set; }

    public string CreatorUser { get; set; }

    public GetLIstQuestionTypeResponse QuestionType { get; set; }

    public virtual ICollection<DefaultAnswerResponse> DefaultAnswers { get; set; }

}
