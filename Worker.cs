using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SaikyoVpn.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SaikyoVpn Service starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                // Placeholder: here you could check tunnel status, attempt reconnection, etc.
                _logger.LogDebug("SaikyoVpn Service heartbeat at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
            _logger.LogInformation("SaikyoVpn Service stopping.");
        }
    }
}
