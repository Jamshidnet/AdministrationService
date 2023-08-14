using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_question")]
[Index("OwnerId", "LanguageId", "ColumnName", Name = "unique_questions", IsUnique = true)]
public partial class TranslateQuestion
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("owner_id")]
    public Guid? OwnerId { get; set; }

    [Column("language_id")]
    public Guid? LanguageId { get; set; }

    [Column("column_name")]
    [StringLength(100)]
    public string ColumnName { get; set; }

    [Column("translate_text")]
    [StringLength(100)]
    public string TranslateText { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("TranslateQuestions")]
    public virtual Language Language { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TranslateQuestions")]
    public virtual Question Owner { get; set; }
}
