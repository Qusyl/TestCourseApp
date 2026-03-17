


using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundService
{
    public class OutboxProcessService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<OutboxProcessService> _logger;

        public OutboxProcessService(IServiceProvider provider, ILogger<OutboxProcessService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {
                await ProcessOutboxService(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
        }

        private async Task ProcessOutboxService(CancellationToken stoppingToken)
        {
            
        }
    }
}
