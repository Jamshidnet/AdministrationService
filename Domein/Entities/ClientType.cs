using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("client_type")]
public partial class ClientType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("type_name")]
    [StringLength(50)]
    public string TypeName { get; set; }

    [InverseProperty("ClientType")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    [InverseProperty("Owner")]
    public virtual ICollection<TranslateClientType> TranslateClientTypes { get; set; }  = new List<TranslateClientType>();
}
