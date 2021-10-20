using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RatingSystem.Models;

#nullable disable

namespace RatingSystem.Data
{
    public partial class RatingDbContext : DbContext
    {
        public RatingDbContext()
        {
        }

        public RatingDbContext(DbContextOptions<RatingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<RatingAverage> RatingAverages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=TS2041;Database=RatingDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.ExternalId, e.UserId, e.Category });

                entity.ToTable("Rating");

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.RatingValue).HasColumnType("money");
            });

            modelBuilder.Entity<RatingAverage>(entity =>
            {
                entity.HasKey(e => new { e.ExternalId, e.Category });

                entity.ToTable("RatingAverage");

                entity.Property(e => e.ExternalId).HasMaxLength(50);

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.AverageRating).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
