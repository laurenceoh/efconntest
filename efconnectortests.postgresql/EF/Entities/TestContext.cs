using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efconnectortests.postgresql.EF.Entities;

public partial class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_pkey");

            entity.ToTable("test");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(now() AT TIME ZONE 'UTC'::text)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
