using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("tbl_loan")]
public partial class TblLoan
{
    [Key]
    [Column("loan_id")]
    public int LoanId { get; set; }

    [Column("loan_amount", TypeName = "decimal(18, 0)")]
    public decimal? LoanAmount { get; set; }

    [Column("loan_type")]
    [StringLength(50)]
    public string? LoanType { get; set; }

    [Column("bank_address")]
    [StringLength(250)]
    public string? BankAddress { get; set; }
}
