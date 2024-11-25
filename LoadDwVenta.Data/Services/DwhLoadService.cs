using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Contexts.Northwind;
using LoadDwVenta.Data.Core;
using LoadDwVenta.Data.Entities.DwVentas;
using LoadDwVenta.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoadDwVenta.Data.Services
{
    public class DwhLoadService: IDwhLoadService
    {

        private readonly DwhVentasContext dwhVentasContext;
        private readonly NorthwindContext northwindContext;

        public DwhLoadService(DwhVentasContext dwhVentasContext, NorthwindContext northwindContext)
        {
            this.dwhVentasContext = dwhVentasContext;
            this.northwindContext = northwindContext;
        }

        public async Task<OperationResult> LoadDwhVentas()
        {

            var operation = new OperationResult();

            try
            {
                var limpieza  = await dwhVentasContext.Procedures.CleanDataAsync();

                var steps = new List<Func<Task<OperationResult>>>
                {
                    LoadEmployees,
                    LoadCategory,
                    LoadCustomers,
                    LoadProducts,
                    LoadShippers,
                    LoadFactClienteAtendidos,
                    LoadFactVentaTotales
                };

                foreach (var step in steps)
                {
                    operation = await step();
                    if (!operation.Success)
                    {
                        return operation;
                    }
                }

                operation.Success = true;
            }
            catch (Exception ex)
            {
                operation.Success = false;
                operation.Message = $"Error inesperado: {ex.Message}";
            }

            return operation;
        }

        public async Task<OperationResult> LoadShippers()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var shippers = await northwindContext.Shippers.Select(ship => new DimShipper
                {
                    ShipperId = ship.ShipperId,
                    CompanyName = ship.CompanyName,
                    Phone = ship.Phone
                })
                .AsNoTracking()
                .ToListAsync();

                await dwhVentasContext.DimShippers.AddRangeAsync(shippers);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;
            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando la dimesion de shippers";
            }

            return operation;
        }

        public async Task<OperationResult> LoadProducts()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var products = await northwindContext.Products.Select(pro => new DimProduct
                {
                    ProductId = pro.ProductId,
                    CategoryId = pro.CategoryId,
                    ProductName = pro.ProductName,
                    UnitPrice = pro.UnitPrice
                })
                .AsNoTracking()
                .ToListAsync();

                await dwhVentasContext.DimProducts.AddRangeAsync(products);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;
            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando la dimesion de productos";
            }

            return operation;
        }

        public async Task<OperationResult> LoadEmployees()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var employees = await northwindContext.Employees
                  .Select(emp => new DimEmployee
                  {
                      EmployeeId = emp.EmployeeId,
                      LastName = emp.LastName,
                      FirstName = emp.FirstName,
                  })
                  .AsNoTracking()
                  .ToListAsync(); // Materializa la consulta


                await dwhVentasContext.DimEmployees.AddRangeAsync(employees);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;
            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando la dimesion de empleados";
            }

            return operation;
        }

        public async Task<OperationResult> LoadCustomers()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var customers = await northwindContext.Customers.Select(cus => new DimCustomer
                {
                    CustomerId = cus.CustomerId,
                    ContactName = cus.ContactName,

                })
                .AsNoTracking()
                .ToListAsync();
                await dwhVentasContext.DimCustomers.AddRangeAsync(customers);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;

            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando la dimesion de customers";
            }

            return operation;
        }

        public async Task<OperationResult> LoadCategory()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var categories = await northwindContext.Categories
                 .Select(emp => new DimCategory
                 {
                     CategoryId = emp.CategoryId,
                     CategoryName = emp.CategoryName,
                     Description = emp.Description
                 })
                 .AsNoTracking()
                 .ToListAsync(); // Materializa la consulta

                await dwhVentasContext.DimCategories.AddRangeAsync(categories);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;

            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando la dimesion de categorias";
            }

            return operation;
        }

        public async Task<OperationResult> LoadFactClienteAtendidos()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var factClientes = await northwindContext.ClienteAtendidosEmpleados
                 .Select(cli => new FactClientesAtendido
                 {
                    EmployeeID = cli.EmployeeId ?? 0,
                    NombreEmpleado = cli.Nombre,
                    TotalClientes = cli.ClienteAtendidos ?? 0
                 })
                 .AsNoTracking()
                 .ToListAsync(); // Materializa la consulta

                await dwhVentasContext.FactClientesAtendidos.AddRangeAsync(factClientes);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;

            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando el fact de clientes atentendidos";
            }

            return operation;
        }

        public async Task<OperationResult> LoadFactVentaTotales()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var factVentas = await northwindContext.OrdenesVentasTotales
                 .Select(ve => new FactOrder
                 {
                     CustomerId = ve.CustomerId,
                     EmployeeId = ve.EmployeeId,
                     OrderDate = ve.OrderDate,
                     ShipVia = ve.ShipVia,
                     ShipCity = ve.ShipCity,
                     ShipCountry = ve.ShipCountry,
                     TotalVentas = ve.TotalVentas,
                     CantidadVentas = ve.CantidadVentas
                 })
                 .AsNoTracking()
                 .ToListAsync(); // Materializa la consulta

                await dwhVentasContext.FactOrders.AddRangeAsync(factVentas);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success = true;

            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando el fact de orders";
            }

            return operation;
        }
    }
}
