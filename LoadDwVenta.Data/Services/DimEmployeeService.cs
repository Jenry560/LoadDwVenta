using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Contexts.Northwind;
using LoadDwVenta.Data.Core;
using LoadDwVenta.Data.Entities.DwVentas;
using LoadDwVenta.Data.Interfaces;

namespace LoadDwVenta.Data.Services
{
    public class DimEmployeeService: IDimEmployeeService
    {
        private readonly DwhVentasContext dwhVentasContext;
        private readonly NorthwindContext northwindContext;

        public DimEmployeeService(DwhVentasContext dwhVentasContext, NorthwindContext northwindContext)
        {
            this.dwhVentasContext = dwhVentasContext;
            this.northwindContext = northwindContext;
        }

        public async Task<OperationResult> LoadEmployees()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var employees = northwindContext.Employees.Select(emp => new DimEmployee()
                {
                    EmployeeId = emp.EmployeeId,
                    LastName = emp.LastName,
                    FirstName = emp.FirstName,

                }).ToList();

                await dwhVentasContext.DimEmployees.AddRangeAsync(employees);
                await dwhVentasContext.SaveChangesAsync();
                operation.Success =true;
            }
            catch (Exception)
            {
                operation.Success = false;
                operation.Message = $"Error cargando la dimesion de producto";
            }
            
            return operation;
        }
    }
}
