using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application_test_repo.Modal
{
    public class AnimalModal
    {
        [Key]
        [Column("animal_id")]
        public int AnimalId { get; set; }

        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }

        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }

        [Column("tag_number")]
        [StringLength(150)]
        public string? TagNumber { get; set; }

        [Column("weight")]
        public int? Weight { get; set; }

        [Column("price", TypeName = "decimal(18, 0)")]
        public decimal? Price { get; set; }

        [Column("produced_born")]
        public int? ProducedBorn { get; set; }

        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }

        [Column("medical_id")]
        public int? MedicalId { get; set; }

        [Column("bred_code")]
        [StringLength(10)]
        public string? BredCode { get; set; }
    }
}
