namespace Application.Common.Models
{
    public class CreateCommandTranslate
    {
        public string TranslateText { get; set; }

        public Guid LanguageId { get; set; }
    }
}
