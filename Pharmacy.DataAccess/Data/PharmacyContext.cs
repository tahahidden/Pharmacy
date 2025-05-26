using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.DataAccess.Data;

public partial class PharmacyContext : DbContext
{
    public PharmacyContext()
    {
    }

    public PharmacyContext(DbContextOptions<PharmacyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<Medicinesinventory> Medicinesinventories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pharmacy;Username=postgres;Password=Abcd1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medicine_pkey");

            entity.ToTable("medicine");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Medicinesinventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medicinesinventory_pkey");

            entity.ToTable("medicinesinventory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ExpirationDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Medicine).WithMany(p => p.Medicinesinventories)
                .HasForeignKey(d => d.MedicineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_medicine_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
