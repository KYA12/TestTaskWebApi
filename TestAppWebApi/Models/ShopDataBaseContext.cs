using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestAppWebApi.Models
{
    public partial class ShopDataBaseContext : DbContext
    {
        public ShopDataBaseContext()
        {

        }

        public ShopDataBaseContext(DbContextOptions<ShopDataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consultant> Consultant { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.Property(e => e.ConsultantId).HasColumnName("ConsultantID");

                entity.Property(e => e.DateHiring)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Consultant)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Consultant_Shop");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.HasIndex(e => e.ShopName)
                    .HasName("AK_ShopName")
                    .IsUnique();

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
