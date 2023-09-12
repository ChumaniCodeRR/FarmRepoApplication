using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_weather")]
public partial class TblWeather
{
    [Key]
    [Column("weather_id")]
    public int WeatherId { get; set; }

    [Column("location")]
    [StringLength(250)]
    public string? Location { get; set; }

    [Column("temperature", TypeName = "decimal(18, 0)")]
    public decimal? Temperature { get; set; }

    [Column("humidity", TypeName = "decimal(18, 0)")]
    public decimal? Humidity { get; set; }
}
