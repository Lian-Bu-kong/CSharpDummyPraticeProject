using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DummyMVCEFModel.Models
{
    public partial class dbEmployeeContext : DbContext
    {
        public dbEmployeeContext()
        {
        }

        public dbEmployeeContext(DbContextOptions<dbEmployeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TDepartment> TDepartment { get; set; }
        public virtual DbSet<TEmployee> TEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename= D:\\GithubDemo\\AspNetCoreDummyTest\\DummyMVCEFModel\\DummyMVCEFModel\\App_Data\\dbEmployee.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TDepartment>(entity =>
            {
                entity.HasKey(e => e.FDepId)
                    .HasName("PK__tmp_ms_x__9D762ED3A27DBBEA");

                entity.ToTable("tDepartment");

                entity.Property(e => e.FDepId).HasColumnName("fDepId");

                entity.Property(e => e.FDepName)
                    .HasColumnName("fDepName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TEmployee>(entity =>
            {
                entity.HasKey(e => e.FEmpId)
                    .HasName("PK__tmp_ms_x__C4A26E9C686898F4");

                entity.ToTable("tEmployee");

                entity.Property(e => e.FEmpId)
                    .HasColumnName("fEmpId")
                    .HasMaxLength(10);

                entity.Property(e => e.FDepId).HasColumnName("fDepId");

                entity.Property(e => e.FName)
                    .HasColumnName("fName")
                    .HasMaxLength(30);

                entity.Property(e => e.FPhone)
                    .HasColumnName("fPhone")
                    .HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
