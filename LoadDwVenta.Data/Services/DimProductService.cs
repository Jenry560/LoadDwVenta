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
    public class DimProductService : IDimProductService
    {
        private readonly DwhVentasContext dwhVentasContext;
        private readonly NorthwindContext northwindContext;

        public DimProductService(DwhVentasContext dwhVentasContext, NorthwindContext northwindContext)
        {
            this.dwhVentasContext = dwhVentasContext;
            this.northwindContext = northwindContext;
        }

        public async Task<OperationResult> LoadProducts()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var products = northwindContext.Products.Select(pro => new DimProduct()
                {
                    ProductId = pro.ProductId,
                    CategoryId = pro.CategoryId,
                    ProductName = pro.ProductName,
                    UnitPrice = pro.UnitPrice
                }).ToList();

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
    }
}
