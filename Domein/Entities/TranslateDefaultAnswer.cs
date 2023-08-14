using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_default_answers")]
[Index("OwnerId", "LanguageId", "ColumnName", Name = "unique_cons_s", IsUnique = true)]
public partial class TranslateDefaultAnswer
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
    [InverseProperty("TranslateDefaultAnswers")]
    public virtual Language Language { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TranslateDefaultAnswers")]
    public virtual DefaultAnswer Owner { get; set; }
}
