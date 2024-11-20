using LoadDwVenta.Data.Contexts.DwhVentas;
using LoadDwVenta.Data.Contexts.Northwind;
using LoadDwVenta.Data.Interfaces;
using LoadDwVenta.Data.Services;
using LoadDwVenta.WorkerServices;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContext<DwhVentasContext>();
builder.Services.AddDbContext<NorthwindContext>();
builder.Services.AddScoped<IDimEmployeeService, DimEmployeeService>();
builder.Services.AddScoped<IDimCategoryService, DimCategoryService>();
builder.Services.AddScoped<IDimCustomerService, DimCustomerService>();
builder.Services.AddScoped<IDimProductService, DimProductService>();
builder.Services.AddScoped<IDimShipperService,DimShipperService >();
builder.Services.AddScoped<DwhLoadService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
