using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("languages")]
public partial class Language
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("language_name")]
    [StringLength(50)]
    public string LanguageName { get; set; }

    [InverseProperty("Language")]
    public virtual ICollection<TranslateCategory> TranslateCategories { get; set; } = new List<TranslateCategory>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslateClientType> TranslateClientTypes { get; set; } = new List<TranslateClientType>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslateDefaultAnswer> TranslateDefaultAnswers { get; set; } = new List<TranslateDefaultAnswer>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslatePermission> TranslatePermissions { get; set; } = new List<TranslatePermission>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslateQuestionType> TranslateQuestionTypes { get; set; } = new List<TranslateQuestionType>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslateQuestion> TranslateQuestions { get; set; } = new List<TranslateQuestion>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslateRole> TranslateRoles { get; set; } = new List<TranslateRole>();

    [InverseProperty("Language")]
    public virtual ICollection<TranslateUserType> TranslateUserTypes { get; set; } = new List<TranslateUserType>();

    [InverseProperty("Language")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
