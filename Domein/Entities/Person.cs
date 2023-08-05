using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("person")]
public partial class Person
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Column("birthdate")]
    public DateOnly Birthdate { get; set; }

    [Column("phone_number")]
    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [Column("quarter_id")]
    public Guid? QuarterId { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [ForeignKey("QuarterId")]
    [InverseProperty("People")]
    public virtual Quarter Quarter { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
