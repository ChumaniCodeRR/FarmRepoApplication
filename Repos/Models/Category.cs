using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos.Models;

[Table("Category")]
public partial class Category
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
