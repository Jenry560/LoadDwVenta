using LoadDwVenta.Data.Entities.Nortwind;
using Microsoft.EntityFrameworkCore;

namespace LoadDwVenta.Data.Contexts.Northwind;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ClienteAtendidosEmpleado> ClienteAtendidosEmpleados { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<NumeroPedidoEmpleado> NumeroPedidoEmpleados { get; set; }

    public DbSet<OrdenesCliente> OrdenesClientes { get; set; }

    public DbSet<OrdenesTransportistum> OrdenesTransportista { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductosMasVendido> ProductosMasVendidos { get; set; }

    public DbSet<ProductosMasVendidosCategorium> ProductosMasVendidosCategoria { get; set; }

    public DbSet<Shipper> Shippers { get; set; }

    public DbSet<VentasCliente> VentasClientes { get; set; }

    public DbSet<VentasEmpleado> VentasEmpleados { get; set; }

    public DbSet<VentasPorRegionPai> VentasPorRegionPais { get; set; }

    public DbSet<VentasTotalesCategoria> VentasTotalesCategorias { get; set; }

    public DbSet<VentasTotalesPerido> VentasTotalesPeridos { get; set; }

    public DbSet<VentasTransportistum> VentasTransportista { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
          => optionsBuilder.UseSqlServer("Data Source=DESKTOP-NDORDVA\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryName, "CategoryName");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(15);
            entity.Property(e => e.Picture).HasColumnType("image");
        });

        modelBuilder.Entity<ClienteAtendidosEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ClienteAtendidosEmpleados", "DWH");

            entity.Property(e => e.ClienteAtendidos).HasColumnName("clienteAtendidos");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.City, "City");

            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.HasIndex(e => e.Region, "Region");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.LastName, "LastName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Extension).HasMaxLength(4);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.HomePhone).HasMaxLength(24);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.PhotoPath).HasMaxLength(255);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
            entity.Property(e => e.Title).HasMaxLength(30);
            entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("FK_Employees_Employees");
        });

        modelBuilder.Entity<NumeroPedidoEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NumeroPedidoEmpleados", "DWH");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.NumeroPedidos).HasColumnName("numeroPedidos");
        });

        modelBuilder.Entity<OrdenesCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrdenesClientes", "DWH");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.NumeroOrdenes).HasColumnName("numeroOrdenes");
        });

        modelBuilder.Entity<OrdenesTransportistum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrdenesTransportista", "DWH");

            entity.Property(e => e.NumeroOrdenes).HasColumnName("numeroOrdenes");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "CategoriesProducts");

            entity.HasIndex(e => e.CategoryId, "CategoryID");

            entity.HasIndex(e => e.ProductName, "ProductName");

            entity.HasIndex(e => e.SupplierId, "SupplierID");

            entity.HasIndex(e => e.SupplierId, "SuppliersProducts");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);
            entity.Property(e => e.ReorderLevel).HasDefaultValue((short)0);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.UnitsInStock).HasDefaultValue((short)0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue((short)0);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<ProductosMasVendido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosMasVendidos", "DWH");

            entity.Property(e => e.CantidadVendida).HasColumnName("cantidadVendida");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
        });

        modelBuilder.Entity<ProductosMasVendidosCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductosMasVendidosCategoria", "DWH");

            entity.Property(e => e.CantidadVendida).HasColumnName("cantidadVendida");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");
            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.Phone).HasMaxLength(24);
        });

        modelBuilder.Entity<VentasCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasClientes", "DWH");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Ventas)
                .HasColumnType("money")
                .HasColumnName("ventas");
        });

        modelBuilder.Entity<VentasEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasEmpleados", "DWH");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Ventas)
                .HasColumnType("money")
                .HasColumnName("ventas");
        });

        modelBuilder.Entity<VentasPorRegionPai>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasPorRegionPais", "DWH");

            entity.Property(e => e.ShipCountry).HasMaxLength(15);
            entity.Property(e => e.Totales)
                .HasColumnType("money")
                .HasColumnName("totales");
        });

        modelBuilder.Entity<VentasTotalesCategoria>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasTotalesCategorias", "DWH");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
        });

        modelBuilder.Entity<VentasTotalesPerido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasTotalesPeridos", "DWH");

            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Mes).HasColumnName("mes");
            entity.Property(e => e.TotalVendido)
                .HasColumnType("money")
                .HasColumnName("totalVendido");
        });

        modelBuilder.Entity<VentasTransportistum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VentasTransportista", "DWH");

            entity.Property(e => e.TotalVendido)
                .HasColumnType("money")
                .HasColumnName("totalVendido");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}