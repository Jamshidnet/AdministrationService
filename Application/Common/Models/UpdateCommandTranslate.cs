namespace Application.Common.Models
{
    public class UpdateCommandTranslate : CreateCommandTranslate
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
    }
}
