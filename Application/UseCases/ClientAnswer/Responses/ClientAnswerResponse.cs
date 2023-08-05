
namespace Application.UseCases.DefaultAnswers.Responses
{
    public class ClientAnswerResponse
    {
        public Guid Id { get; set; }

        public string AnswerText { get; set; }

        public Guid DocId { get; set; }

        public string Question { get; set; }
    }
}
