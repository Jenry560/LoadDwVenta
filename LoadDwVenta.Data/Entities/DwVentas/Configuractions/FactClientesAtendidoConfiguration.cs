﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace LoadDwVenta.Data.Entities.DwVentas.Configuractions
{
    public partial class FactClientesAtendidoConfiguration : IEntityTypeConfiguration<FactClientesAtendido>
    {
        public void Configure(EntityTypeBuilder<FactClientesAtendido> entity)
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__FactClie__C7E58F3CBF4E87A4");

            entity.Property(e => e.EmployeeID)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FactClientesAtendido> entity);
    }
}
