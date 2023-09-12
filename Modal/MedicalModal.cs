using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application_test_repo.Modal
{
    public class MedicalModal
    {
        [Key]
        [Column("medical_id")]
        public int MedicalId { get; set; }

        [Column("problem_descrip")]
        [StringLength(400)]
        public string? ProblemDescrip { get; set; }

        [Column("solution_descrip")]
        [StringLength(400)]
        public string? SolutionDescrip { get; set; }

        [Column("date", TypeName = "date")]
        public DateTime? Date { get; set; }

        [Column("dosage_needed")]
        [StringLength(200)]
        public string? DosageNeeded { get; set; }
    }
}
