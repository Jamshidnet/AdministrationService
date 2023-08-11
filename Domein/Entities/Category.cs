using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domein.Entities;

[Table("categories")]
public partial class Category
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("category_name")]
    [StringLength(50)]
    public string CategoryName { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Categories")]
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
