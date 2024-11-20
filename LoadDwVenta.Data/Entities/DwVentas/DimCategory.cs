

namespace LoadDwVenta.Data.Entities.DwVentas;

public partial class DimCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string Description { get; set; }

    public virtual ICollection<DimProduct> DimProducts { get; set; } = new List<DimProduct>();
}