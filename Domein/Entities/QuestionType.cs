using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domein.Entities;

[Table("question_type")]
public partial class QuestionType
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("question_type_name")]
    [StringLength(50)]
    public string QuestionTypeName { get; set; }

    [InverseProperty("QuestionType")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
