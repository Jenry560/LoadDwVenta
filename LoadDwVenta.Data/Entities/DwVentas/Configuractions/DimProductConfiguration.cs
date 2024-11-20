﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LoadDwVenta.Data.Entities.DwVentas.Configuractions
{
    public partial class DimProductConfiguration : IEntityTypeConfiguration<DimProduct>
    {
        public void Configure(EntityTypeBuilder<DimProduct> entity)
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Products");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.DimProducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DimProduct> entity);
    }
}