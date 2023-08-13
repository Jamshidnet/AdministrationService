using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("client_answers")]
public partial class ClientAnswer
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("answer_text")]
    public string AnswerText { get; set; }

    [Column("default_answer_id")]
    public Guid? DefaultAnswerId { get; set; }

    [Column("doc_id")]
    public Guid DocId { get; set; }

    [Column("question_id")]
    public Guid QuestionId { get; set; }

    [ForeignKey("DefaultAnswerId")]
    [InverseProperty("ClientAnswers")]
    public virtual DefaultAnswer DefaultAnswer { get; set; }

    [ForeignKey("DocId")]
    [InverseProperty("ClientAnswers")]
    public virtual Doc Doc { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("ClientAnswers")]
    public virtual Question Question { get; set; }
}
