using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("questions")]
public partial class Question
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("question_text")]
    public string QuestionText { get; set; }

    [Column("category_id")]
    public Guid CategoryId { get; set; }

    [Column("creator_user_id")]
    public Guid CreatorUserId { get; set; }

    [Column("question_type_id")]
    public Guid? QuestionTypeId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Questions")]
    public virtual Category Category { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<ClientAnswer> ClientAnswers { get; set; } = new List<ClientAnswer>();

    [ForeignKey("CreatorUserId")]
    [InverseProperty("Questions")]
    public virtual User CreatorUser { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<DefaultAnswer> DefaultAnswers { get; set; } = new List<DefaultAnswer>();

    [ForeignKey("QuestionTypeId")]
    [InverseProperty("Questions")]
    public virtual QuestionType QuestionType { get; set; }
}
