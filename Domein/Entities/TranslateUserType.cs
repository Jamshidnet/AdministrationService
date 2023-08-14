using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_user_type")]
[Index("OwnerId", "ColumnName", "LanguageId", Name = "unique_user_type", IsUnique = true)]
public partial class TranslateUserType
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
    [InverseProperty("TranslateUserTypes")]
    public virtual Language Language { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("TranslateUserTypes")]
    public virtual UserType Owner { get; set; }
}
