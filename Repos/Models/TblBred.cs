using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_breds")]
public partial class TblBred
{
    [Key]
    [Column("bred_code")]
    [StringLength(10)]
    public string BredCode { get; set; } = null!;

    [Column("species")]
    [StringLength(200)]
    public string? Species { get; set; }

    [Column("tags")]
    [StringLength(50)]
    public string? Tags { get; set; }

    [Column("bred_date", TypeName = "date")]
    public DateTime? BredDate { get; set; }
}
