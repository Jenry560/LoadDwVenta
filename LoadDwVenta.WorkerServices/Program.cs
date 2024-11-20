using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Contexts.Northwind;
using LoadDwVenta.Data.Interfaces;
using LoadDwVenta.Data.Services;
using LoadDwVenta.WorkerServices;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<DwhVentasContext>();
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddScoped<IDimEmployeeService, DimEmployeeService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
