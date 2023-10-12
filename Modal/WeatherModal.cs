using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Modal
{
    public class WeatherModal
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
}
