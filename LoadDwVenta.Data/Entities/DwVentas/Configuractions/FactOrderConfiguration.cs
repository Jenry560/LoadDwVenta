﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoadDwVenta.Data.Entities.DwVentas.Configuractions
{
    public partial class FactOrderConfiguration : IEntityTypeConfiguration<FactOrder>
    {
        public void Configure(EntityTypeBuilder<FactOrder> entity)
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Orders");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.ShipCity).HasMaxLength(15);
            entity.Property(e => e.ShipCountry).HasMaxLength(15);
            entity.Property(e => e.TotalVentas).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.FactOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.FactOrders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.FactOrders)
                .HasForeignKey(d => d.ShipVia)
                .HasConstraintName("FK_Orders_Shippers");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FactOrder> entity);
    }
}
