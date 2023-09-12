using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_fertilizer")]
public partial class TblFertilizer
{
    [Key]
    [Column("fertilizer_id")]
    public int FertilizerId { get; set; }

    [Column("fname")]
    [StringLength(50)]
    public string? Fname { get; set; }

    [Column("frate", TypeName = "decimal(18, 0)")]
    public decimal? Frate { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }

    [Column("crop_id")]
    public int? CropId { get; set; }
}
