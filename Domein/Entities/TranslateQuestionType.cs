using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_question_type")]
[Index("OwnerId", "LanguageId", "ColumnName", Name = "unique_question_type", IsUnique = true)]
public partial class TranslateQuestionType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("owner_id")]
    public Guid? OwnerId { get; set; }

    [Column("language_id")]
    public Guid? LanguageId { get; set; }

    [Column("translate_text")]
    [StringLength(100)]
    public string TranslateText { get; set; }

    [Column("column_name")]
    [StringLength(100)]
    public string ColumnName { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("TranslateQuestionTypes")]
    public virtual Language Language { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TranslateQuestionTypes")]
    public virtual QuestionType Owner { get; set; }
}
