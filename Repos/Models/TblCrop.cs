using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_crop")]
public partial class TblCrop
{
    [Key]
    [Column("crop_id")]
    public int CropId { get; set; }

    [Column("crop_name")]
    [StringLength(100)]
    public string? CropName { get; set; }

    [Column("quantity")]
    public int? Quantity { get; set; }
}
