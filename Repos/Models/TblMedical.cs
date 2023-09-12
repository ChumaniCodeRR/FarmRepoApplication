using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_medical")]
public partial class TblMedical
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
