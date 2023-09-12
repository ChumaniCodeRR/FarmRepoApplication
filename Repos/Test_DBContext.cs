using System;
using System.Collections.Generic;
using Application_test_repo.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Repos;

public partial class Test_DBContext : DbContext
{
    public Test_DBContext()
    {
    }

    public Test_DBContext(DbContextOptions<Test_DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<TblAgriExpectAdviser> TblAgriExpectAdvisers { get; set; }

    public virtual DbSet<TblAnimal> TblAnimals { get; set; }

    public virtual DbSet<TblBred> TblBreds { get; set; }

    public virtual DbSet<TblCrop> TblCrops { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblDesignation> TblDesignations { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    public virtual DbSet<TblFertilizer> TblFertilizers { get; set; }

    public virtual DbSet<TblLoan> TblLoans { get; set; }

    public virtual DbSet<TblMarketDetail> TblMarketDetails { get; set; }

    public virtual DbSet<TblMedical> TblMedicals { get; set; }

    public virtual DbSet<TblMenu> TblMenus { get; set; }

    public virtual DbSet<TblPermission> TblPermissions { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductimage> TblProductimages { get; set; }

    public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSeed> TblSeeds { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblWeather> TblWeathers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAnimal>(entity =>
        {
            entity.Property(e => e.BredCode).IsFixedLength();
            entity.Property(e => e.Gender).IsFixedLength();
        });

      /*  modelBuilder.Entity<TblBred>(entity =>
        {
            entity.Property(e => e.BredCode).IsFixedLength();
        }); */

        modelBuilder.Entity<TblCrop>(entity =>
        {
            entity.HasKey(e => e.CropId).HasName("PK_Crop");
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.Property(e => e.CreditLimit).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<TblMedical>(entity =>
        {
            entity.HasKey(e => e.MedicalId).HasName("PK_tbl_bred");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
