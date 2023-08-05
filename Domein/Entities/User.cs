using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domein.Entities;


[Table("users")]
[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("username")]
    [StringLength(20)]
    public string Username { get; set; }

    [Required]
    [Column("password")]
    [StringLength(100)]
    public string Password { get; set; }

    [Column("salt_id")]
    public Guid SaltId { get; set; }

    [Column("person_id")]
    public Guid PersonId { get; set; }

    [Column("refresh_token")]
    public string RefreshToken { get; set; }

    [Column("expires_date")]
    public DateTime? ExpiresDate { get; set; }

    [Column("user_type_id")]
    public Guid UserTypeId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();

    [ForeignKey("PersonId")]
    [InverseProperty("Users")]
    public virtual Person Person { get; set; }

    [InverseProperty("CreatorUser")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [ForeignKey("UserTypeId")]
    [InverseProperty("Users")]
    public virtual UserType UserType { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
