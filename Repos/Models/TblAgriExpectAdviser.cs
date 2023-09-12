using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_agri_expect_adviser")]
public partial class TblAgriExpectAdviser
{
    [Key]
    [Column("expert_id")]
    public int ExpertId { get; set; }

    [Column("contact_details")]
    [StringLength(50)]
    public string? ContactDetails { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }
}
