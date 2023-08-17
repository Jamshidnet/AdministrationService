using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("roles")]
public partial class Role
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("role_name")]
    [StringLength(20)]
    public string RoleName { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<TranslateRole> TranslateRoles { get; set; } = new List<TranslateRole>();

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    [ForeignKey("RoleId")]
    [InverseProperty("Roles")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
