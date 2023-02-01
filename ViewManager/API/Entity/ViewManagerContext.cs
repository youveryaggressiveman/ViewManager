using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Entity
{
    public partial class ViewManagerContext : DbContext
    {
        public ViewManagerContext()
        {
        }

        public ViewManagerContext(DbContextOptions<ViewManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<LogByOffice> LogByOffices { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Specialization> Specializations { get; set; } = null!;
        public virtual DbSet<StatusByLog> StatusByLogs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=32-11\\SQLEXPRESS;Database=ViewManager;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Value).HasMaxLength(200);
            });

            modelBuilder.Entity<LogByOffice>(entity =>
            {
                entity.ToTable("LogByOffice");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.OfficeId)
                    .HasMaxLength(10)
                    .HasColumnName("OfficeID");

                entity.Property(e => e.StatusByLogId).HasColumnName("StatusByLogID");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Log)
                    .WithMany(p => p.LogByOffices)
                    .HasForeignKey(d => d.LogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LogByOffi__LogID__32E0915F");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.LogByOffices)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LogByOffi__Offic__31EC6D26");

                entity.HasOne(d => d.StatusByLog)
                    .WithMany(p => p.LogByOffices)
                    .HasForeignKey(d => d.StatusByLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LogByOffi__Statu__33D4B598");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Office");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID");

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Value).HasMaxLength(50);
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("Specialization");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Value).HasMaxLength(50);
            });

            modelBuilder.Entity<StatusByLog>(entity =>
            {
                entity.ToTable("StatusByLog");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Value).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .HasMaxLength(150)
                    .HasColumnName("ID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.OfficeId)
                    .HasMaxLength(10)
                    .HasColumnName("OfficeID");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.SecondName).HasMaxLength(50);

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__OfficeID__2B3F6F97");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleID__2A4B4B5E");

                entity.HasMany(d => d.Specializations)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserOfSpecialization",
                        l => l.HasOne<Specialization>().WithMany().HasForeignKey("SpecializationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserOfSpe__Speci__37A5467C"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserOfSpe__UserI__36B12243"),
                        j =>
                        {
                            j.HasKey("UserId", "SpecializationId").HasName("PK__UserOfSp__F20851289F2292D9");

                            j.ToTable("UserOfSpecialization");

                            j.IndexerProperty<string>("UserId").HasMaxLength(50).HasColumnName("UserID");

                            j.IndexerProperty<int>("SpecializationId").HasColumnName("SpecializationID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
