using LoadDwVenta.Data.Core;

namespace LoadDwVenta.Data.Interfaces
{
    public interface IDimCustomerService
    {
        Task<OperationResult> LoadCustomers();
    }
}
