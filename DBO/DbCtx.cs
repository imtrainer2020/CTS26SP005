using System;
using System.Collections.Generic;
using DBO.Models;
using Microsoft.EntityFrameworkCore;

namespace DBO;

public partial class DbCtx : DbContext
{
    readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DFSA002Project;Trusted_Connection=True;TrustServerCertificate=True";
    public DbCtx()
    {
    }

    public DbCtx(DbContextOptions<DbCtx> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tmp_ms_x__1788CC4CA58FB2AE");

            entity.HasIndex(e => e.Username, "UQ__tmp_ms_x__536C85E4F9FC5E65").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_UserRole");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UDId).HasName("PK__tmp_ms_x__B1A79D99E6BAD2D2");

            entity.ToTable("UserDetail");

            entity.Property(e => e.UDId).HasColumnName("UDId");
            entity.Property(e => e.UDAddress)
                .HasMaxLength(100)
                .HasColumnName("UDAddress");
            entity.Property(e => e.UDCity)
                .HasMaxLength(50)
                .HasColumnName("UDCity");
            entity.Property(e => e.UDPhone)
                .HasMaxLength(20)
                .HasColumnName("UDPhone");
            entity.Property(e => e.UDPhoto).HasColumnName("UDPhoto");
            entity.Property(e => e.UDPostCode)
                .HasMaxLength(20)
                .HasColumnName("UDPostCode");

            entity.HasOne(d => d.User).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetail_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE1A187184B8");

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
