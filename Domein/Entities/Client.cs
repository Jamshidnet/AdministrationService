using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;


[Table("clients")]
public partial class Client
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("client_type_id")]
    public Guid ClientTypeId { get; set; }

    [Column("person_id")]
    public Guid PersonId { get; set; }

    [ForeignKey("ClientTypeId")]
    [InverseProperty("Clients")]
    public virtual ClientType ClientType { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Doc> Docs { get; set; } = new List<Doc>();

    [ForeignKey("PersonId")]
    [InverseProperty("Clients")]
    public virtual Person Person { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Clients")]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
