using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("quarters")]
public partial class Quarter
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("quarter_name")]
    [StringLength(20)]
    public string QuarterName { get; set; }

    [Column("district_id")]
    public Guid DistrictId { get; set; }

    [ForeignKey("DistrictId")]
    [InverseProperty("Quarters")]
    public virtual District District { get; set; }

    [InverseProperty("Quarter")]
    public virtual ICollection<Person> People { get; set; } 
}
