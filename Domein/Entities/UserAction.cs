using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("user_actions")]
public partial class UserAction
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("action_name")]
    [StringLength(50)]
    public string ActionName { get; set; }

    [Column("table_id")]
    public Guid? TableId { get; set; }

    [Column("action_time", TypeName = "timestamp without time zone")]
    public DateTime? ActionTime { get; set; }

    [ForeignKey("TableId")]
    [InverseProperty("UserActions")]
    public virtual SysTable Table { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserActions")]
    public virtual User User { get; set; }
}
