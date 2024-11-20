namespace LoadDwVenta.Data.Entities.DwVentas;

public partial class DimShipper
{
    public int ShipperId { get; set; }

    public string CompanyName { get; set; }

    public string Phone { get; set; }

    public virtual ICollection<FactOrder> FactOrders { get; set; } = new List<FactOrder>();
}