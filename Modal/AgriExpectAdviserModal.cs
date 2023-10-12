using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Modal
{
    public class AgriExpectAdviserModal
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
}
