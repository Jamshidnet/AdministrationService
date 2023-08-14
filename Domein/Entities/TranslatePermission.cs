using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_permissions")]
[Index("OwnerId", "LanguageId", "ColumnName", Name = "unique_permission", IsUnique = true)]
public partial class TranslatePermission
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
    [InverseProperty("TranslatePermissions")]
    public virtual Language Language { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TranslatePermissions")]
    public virtual Permission Owner { get; set; }
}
