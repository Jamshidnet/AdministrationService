using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("districts")]
public partial class District
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("district_name")]
    [StringLength(20)]
    public string DistrictName { get; set; }

    [Column("region_id")]
    public Guid RegionId { get; set; }

    [InverseProperty("District")]
    public virtual ICollection<Quarter> Quarters { get; set; } = new List<Quarter>();

    [ForeignKey("RegionId")]
    [InverseProperty("Districts")]
    public virtual Region Region { get; set; }
}
