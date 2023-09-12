using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_seed")]
public partial class TblSeed
{
    [Key]
    [Column("seed_id")]
    public int SeedId { get; set; }

    [Column("rate", TypeName = "decimal(18, 0)")]
    public decimal? Rate { get; set; }

    [Column("category")]
    [StringLength(50)]
    public string? Category { get; set; }

    [Column("seed_name")]
    [StringLength(50)]
    public string? SeedName { get; set; }

    [Column("type")]
    [StringLength(50)]
    public string? Type { get; set; }
}
