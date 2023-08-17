using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("docs")]
public partial class Doc
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("client_id")]
    public Guid ClientId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("taken_date")]
    public DateOnly? TakenDate { get; set; }

    [Column("longitude")]
    [Precision(100, 0)]
    public decimal? Longitude { get; set; }

    [Column("latitude")]
    [Precision(100, 0)]
    public decimal? Latitude { get; set; }

    [Column("device")]
    [StringLength(70)]
    public string Device { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Docs")]
    public virtual Client Client { get; set; }

    [InverseProperty("Doc")]
    public virtual ICollection<ClientAnswer> ClientAnswers { get; set; } = new List<ClientAnswer>();

    [InverseProperty("Doc")]
    public virtual ICollection<DocChangeLog> DocChangeLogs { get; set; } = new List<DocChangeLog>();

    [ForeignKey("UserId")]
    [InverseProperty("Docs")]
    public virtual User User { get; set; }
}
