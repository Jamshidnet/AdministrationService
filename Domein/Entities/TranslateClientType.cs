using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_client_type")]
[Index("OwnerId", "LanguageId", "ColumnName", Name = "unique_cons", IsUnique = true)]
public partial class TranslateClientType
{

    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("owner_id")]
    [ForeignKey("Owner")]
    public Guid? OwnerId { get; set; }

    [Column("language_id")]
    [ForeignKey("Language")]
    public Guid? LanguageId { get; set; }

    [Column("column_name")]
    [StringLength(100)]
    public string ColumnName { get; set; }

    [Column("translate_text")]
    [StringLength(100)]
    public string TranslateText { get; set; }

    [InverseProperty("TranslateClientTypes")]
    public virtual Language Language { get; set; }

    [InverseProperty("TranslateClientTypes")]
    public virtual ClientType Owner { get; set; }
}
