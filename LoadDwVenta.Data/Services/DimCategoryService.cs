using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Contexts.Northwind;
using LoadDwVenta.Data.Core;
using LoadDwVenta.Data.Entities.DwVentas;
using LoadDwVenta.Data.Interfaces;


namespace LoadDwVenta.Data.Services
{
    public class DimCategoryService : IDimCategoryService
    {
        private readonly DwhVentasContext dwhVentasContext;
        private readonly NorthwindContext northwindContext;
        public DimCategoryService(DwhVentasContext dwhVentasContext, NorthwindContext northwindContext)
        {
            this.dwhVentasContext = dwhVentasContext;
            this.northwindContext = northwindContext;
        }

        public async Task<OperationResult> LoadCategory()
        {
            OperationResult operation = new OperationResult();
            try
            {
                var categories = northwindContext.Categories.Select(emp => new DimCategory()
                {
                    CategoryId = emp.CategoryId,
                    CategoryName = emp.CategoryName,
                    Description = emp.Description
                    
                }).ToList();
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
    }
}
