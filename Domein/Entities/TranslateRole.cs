using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_role")]
[Index("OwnerId", "ColumnName", "LanguageId", Name = "unique_role", IsUnique = true)]
public partial class TranslateRole
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
    [InverseProperty("TranslateRoles")]
    public virtual Language Language { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TranslateRoles")]
    public virtual Role Owner { get; set; }
}
