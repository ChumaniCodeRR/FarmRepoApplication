using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Application_test_repo.Modal
{
    public class CropModal
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
}
