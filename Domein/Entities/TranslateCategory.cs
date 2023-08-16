using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("translate_categories")]
/*[Index("OwnerId", "ColumnName", "LangaugeId", Name = "value_unique", IsUnique = true)]*/
public partial class TranslateCategory
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("owner_id")]
    [ForeignKey("Owner")]
    public Guid? OwnerId { get; set; }

    [Column("translate_text")]
    [StringLength(100)]
    public string TranslateText { get; set; }

    [Column("column_name")]
    [StringLength(100)]
    public string ColumnName { get; set; }

    [Column("language_id")]
    [ForeignKey("Language")]
    public Guid? LanguageId { get; set; }

    [InverseProperty("TranslateCategories")]
    public virtual Language Language { get; set; }

    [InverseProperty("TranslateCategories")]
    public virtual Category Owner { get; set; }
}
