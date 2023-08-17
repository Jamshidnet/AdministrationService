using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("regions")]
public partial class Region
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("region_name")]
    [StringLength(20)]
    public string RegionName { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
