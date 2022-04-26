using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Xero.Products.DataLayer.Data
{
    public partial class ProductsContext : DbContext
    {
        public ProductsContext()
        {
        }

        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductOption> ProductOptions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("NOCASE");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.DeliveryPrice).HasColumnType("decimal(4,2)");

                entity.Property(e => e.Description).HasColumnType("varchar(35)");

                entity.Property(e => e.Id).HasColumnType("varchar(36)");

                entity.Property(e => e.Name).HasColumnType("varchar(17)");

                entity.Property(e => e.Price).HasColumnType("decimal(6,2)");
            });

            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Description).HasColumnType("varchar(23)");

                entity.Property(e => e.Id).HasColumnType("varchar(36)");

                entity.Property(e => e.Name).HasColumnType("varchar(9)");

                entity.Property(e => e.ProductId).HasColumnType("varchar(36)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
