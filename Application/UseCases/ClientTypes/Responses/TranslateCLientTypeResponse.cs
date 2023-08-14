namespace Application.UseCases.ClientTypes.Responses
{
    public class TranslateCLientTypeResponse
    {
        public Guid Id { get; set; }

        public Guid? OwnerId { get; set; }

        public Guid? LanguageId { get; set; }

        public string ColumnName { get; set; }

        public string TranslateText { get; set; }
    }
}
