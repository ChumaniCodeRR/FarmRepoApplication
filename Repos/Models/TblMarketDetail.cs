using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_market_details")]
public partial class TblMarketDetail
{
    [Key]
    [Column("market_id")]
    public int MarketId { get; set; }

    [Column("market_address")]
    [StringLength(150)]
    public string? MarketAddress { get; set; }

    [Column("district_code")]
    [StringLength(50)]
    public string? DistrictCode { get; set; }
}
