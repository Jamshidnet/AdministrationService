using Application.UseCases.Questions.Responses;

namespace Application.UseCases.Categories.Responses;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; }

    public virtual ICollection<QuestionResponse> Questions { get; set; } 
}
