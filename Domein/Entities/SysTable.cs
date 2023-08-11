using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domein.Entities;

[Table("sys_tables")]
public partial class SysTable
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("table_id")]
    public int? TableId { get; set; }

    [InverseProperty("Table")]
    public virtual ICollection<DocChangeLog> DocChangeLogs { get; set; } = new List<DocChangeLog>();

    [InverseProperty("Table")]
    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();
}
