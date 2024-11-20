


namespace LoadDwVenta.Data.Contexts.DwhVentas
{
    public partial interface IDwhVentasContextProcedures
    {
        Task<int> CleanDataAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> LoadCustomersAsync(string ContactName, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> LoadEmployeeAsync(string LastName, string FirstName, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
