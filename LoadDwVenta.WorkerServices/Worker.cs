using LoadDwVenta.Data.Interfaces;
using LoadDwVenta.Data.Services;

namespace LoadDwVenta.WorkerServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var databaseService = scope.ServiceProvider.GetRequiredService<DwhLoadService>();
                        var result = await databaseService.LoadDwhVentas();
                        if (result.Success)
                        {
                            _logger.LogInformation(result.Message);
                        }
                        else
                        {
                            _logger.LogError(result.Message);
                        }
                        return;


                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing worker");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
