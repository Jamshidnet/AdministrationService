using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domein.Entities;

[Table("doc_change_log")]
public partial class DocChangeLog
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("table_id")]
    public Guid? TableId { get; set; }

    [Column("doc_id")]
    public Guid? DocId { get; set; }

    [Column("date_at")]
    public DateOnly? DateAt { get; set; }

    [Column("action_name")]
    [StringLength(50)]
    public string ActionName { get; set; }


    [ForeignKey("DocId")]
    [InverseProperty("DocChangeLogs")]
    public virtual Doc Doc { get; set; }

    [ForeignKey("TableId")]
    [InverseProperty("DocChangeLogs")]
    public virtual SysTable Table { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("DocChangeLogs")]
    public virtual User User { get; set; }
}
