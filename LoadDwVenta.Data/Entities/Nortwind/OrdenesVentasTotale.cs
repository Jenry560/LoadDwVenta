

namespace LoadDwVenta.Data.Entities.Nortwind;

public partial class OrdenesVentasTotale
{
    public int? EmployeeId { get; set; }

    public string CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? ShipVia { get; set; }

    public string ShipCity { get; set; }

    public string ShipCountry { get; set; }

    public decimal? TotalVentas { get; set; }

    public int? CantidadVentas { get; set; }
}