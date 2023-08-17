using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("question_type")]
public partial class QuestionType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("question_type_name")]
    [StringLength(50)]
    public string QuestionTypeName { get; set; }

    [InverseProperty("QuestionType")]
    public virtual ICollection<Question> Questions { get; set; } 

    [InverseProperty("Owner")]
    public virtual ICollection<TranslateQuestionType> TranslateQuestionTypes { get; set; } 
}
