using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Contexts.Northwind;
using LoadDwVenta.Data.Core;
using LoadDwVenta.Data.Entities.DwVentas;
using LoadDwVenta.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDwVenta.Data.Services
{
    public class DimCustomerService : IDimCustomerService
    {
        private readonly DwhVentasContext dwhVentasContext;
        private readonly NorthwindContext northwindContext;

        public DimCustomerService(DwhVentasContext dwhVentasContext, NorthwindContext northwindContext)
        {
            this.dwhVentasContext = dwhVentasContext;
            this.northwindContext = northwindContext;
        }

        public async Task<OperationResult> LoadCustomers()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var customers = northwindContext.Customers.Select(cus => new DimCustomer()
                {
                   CustomerId = cus.CustomerId,
                   ContactName = cus.ContactName,

                }).ToList();
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
    }
}
