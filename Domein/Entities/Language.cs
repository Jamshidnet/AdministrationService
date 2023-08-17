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
    public virtual ICollection<TranslateCategory> TranslateCategories { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<TranslateClientType> TranslateClientTypes { get; set; } 
    [InverseProperty("Language")]
    public virtual ICollection<TranslateDefaultAnswer> TranslateDefaultAnswers { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<TranslatePermission> TranslatePermissions { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<TranslateQuestionType> TranslateQuestionTypes { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<TranslateQuestion> TranslateQuestions { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<TranslateRole> TranslateRoles { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<TranslateUserType> TranslateUserTypes { get; set; } 

    [InverseProperty("Language")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
