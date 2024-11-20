using LoadDwVenta.Data.Core;


namespace LoadDwVenta.Data.Interfaces
{
    public interface IDimShipperService
    {
        Task<OperationResult> LoadShippers();
    }
}
