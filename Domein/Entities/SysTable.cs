using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("sys_tables")]
public partial class SysTable
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("table_id")]
    public int? TableId { get; set; }

    [Column("table_name")]
    [StringLength(50)]
    public string TableName { get; set; }

    [InverseProperty("Table")]
    public virtual ICollection<DocChangeLog> DocChangeLogs { get; set; } = new List<DocChangeLog>();

    [InverseProperty("Table")]
    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();
}
