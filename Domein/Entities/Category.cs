using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("categories")]
public partial class Category
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("category_name")]
    [StringLength(50)]
    public string CategoryName { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Question> Questions { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<TranslateCategory> TranslateCategories { get; set; } 

    [ForeignKey("CategoryId")]
    [InverseProperty("Categories")]
    public virtual ICollection<Client> Clients { get; set; }
}
