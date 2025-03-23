using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFGetStarted.Models;

[NotMapped]
[Table("blog-metadata")]
class BlogMetadata
{
    //[Key]
    public int Id { get; set; }

    [NotMapped]
    [Column("loaded-from-database")]
    public DateTime LoadedFromDatabase { get; set; }


    [Column(TypeName = "decimal(5, 2)")]
    public decimal Rating { get; set; }


    [Precision(14, 2)]
    public decimal Score { get; set; }


    [MaxLength(500)]
    public string Url { get; set; }


    public DateTime Created { get; set; }
}
