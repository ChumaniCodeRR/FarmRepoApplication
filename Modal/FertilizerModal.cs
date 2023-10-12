using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Modal
{
    public class FertilizerModal
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
}
