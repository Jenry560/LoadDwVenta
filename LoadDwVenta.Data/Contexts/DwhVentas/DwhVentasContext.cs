using LoadDwVenta.Data.Entities.DwVentas;
using Microsoft.EntityFrameworkCore;
using LoadDwVenta.Data.Entities.DwVentas.Configuractions;
using LoadDwVenta.Data.Contexts.Northwind;

namespace LoadDwVenta.Data.Contexts.DwhVentas
{
    public partial class DwhVentasContext : DbContext
    {
        public DwhVentasContext()
        {
        }

        public DwhVentasContext(DbContextOptions<DwhVentasContext> options)
            : base(options)
        {
        }
        public DbSet<DimCategory> DimCategories { get; set; }

        public DbSet<DimCustomer> DimCustomers { get; set; }

        public DbSet<DimEmployee> DimEmployees { get; set; }

        public DbSet<DimProduct> DimProducts { get; set; }

        public DbSet<DimShipper> DimShippers { get; set; }

        public DbSet<FactClientesAtendido> FactClientesAtendidos { get; set; }

        public DbSet<FactOrder> FactOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NDORDVA\\SQLEXPRESS;Initial Catalog=DWHNorthwindOrders;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DimCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DimCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new DimEmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DimProductConfiguration());
            modelBuilder.ApplyConfiguration(new DimShipperConfiguration());
            modelBuilder.ApplyConfiguration(new FactClientesAtendidoConfiguration());
            modelBuilder.ApplyConfiguration(new FactOrderConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
