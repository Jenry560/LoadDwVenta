using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Core;
using LoadDwVenta.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDwVenta.Data.Services
{
    public class DwhLoadService
    {
        private readonly IDimEmployeeService _dimEmployeeService;
        private readonly IDimCategoryService _dimCategoryService;
        private readonly IDimCustomerService _dimCustomerService;
        private readonly IDimProductService _dimProductService;
        private readonly IDimShipperService _dimShipperService;
        private readonly DwhVentasContext _dwhVentasContext;

        public DwhLoadService(IDimEmployeeService dimEmployeeService, IDimCategoryService dimCategoryService,
        IDimCustomerService dimCustomerService, IDimProductService dimProductService, IDimShipperService dimShipperService, DwhVentasContext dwhVentasContext)
        {
            _dimEmployeeService = dimEmployeeService;
            _dimCategoryService = dimCategoryService;
            _dimCustomerService = dimCustomerService;
            _dimProductService = dimProductService;
            _dimShipperService = dimShipperService;
            _dwhVentasContext = dwhVentasContext;
        }


        public async Task<OperationResult> LoadDwhVentas()
        {

            var operation = new OperationResult();

            try
            {
                var limpieza  = await _dwhVentasContext.Procedures.CleanDataAsync();

                var steps = new List<Func<Task<OperationResult>>>
                {
                    _dimEmployeeService.LoadEmployees,
                    _dimCategoryService.LoadCategory,
                    _dimCustomerService.LoadCustomers,
                    _dimProductService.LoadProducts,
                    _dimShipperService.LoadShippers
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
    }
}
