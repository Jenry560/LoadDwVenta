

namespace LoadDwVenta.Data.Entities.DwVentas;

public partial class DimEmployee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public virtual ICollection<FactOrder> FactOrders { get; set; } = new List<FactOrder>();
}