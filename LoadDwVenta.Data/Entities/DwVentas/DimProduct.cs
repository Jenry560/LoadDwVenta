

namespace LoadDwVenta.Data.Entities.DwVentas;

public partial class DimProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public int? CategoryId { get; set; }

    public decimal? UnitPrice { get; set; }

    public virtual DimCategory Category { get; set; }
}