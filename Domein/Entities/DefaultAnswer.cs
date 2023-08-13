using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;
[Table("default_answers")]
public partial class DefaultAnswer
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("question_id")]
    public Guid QuestionId { get; set; }

    [Required]
    [Column("answer_text")]
    public string AnswerText { get; set; }

    [InverseProperty("DefaultAnswer")]
    public virtual ICollection<ClientAnswer> ClientAnswers { get; set; } = new List<ClientAnswer>();

    [ForeignKey("QuestionId")]
    [InverseProperty("DefaultAnswers")]
    public virtual Question Question { get; set; }
}
