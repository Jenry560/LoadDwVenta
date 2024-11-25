

using LoadDwVenta.Data.Core;

namespace LoadDwVenta.Data.Interfaces
{
    public interface  IDwhLoadService
    {
        Task<OperationResult> LoadDwhVentas();
        Task<OperationResult> LoadShippers();
        Task<OperationResult> LoadProducts();
        Task<OperationResult> LoadEmployees();
        Task<OperationResult> LoadCustomers();
        Task<OperationResult> LoadCategory();
        Task<OperationResult> LoadFactClienteAtendidos();
        Task<OperationResult> LoadFactVentaTotales();
    }
}
