


using Application.Interface;
using Domain.Events;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
            var scope = _provider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
            var messages = await context.Messages.Where(m => m.ProcessedOn == null).OrderBy(x => x.OccurredOn).Take(20).ToListAsync();
            foreach (var message in messages) {
                try
                {
                    var type = Type.GetType(message.Type);
                    if (type == null) throw new Exception("Event type is not found");
                    var domainEvent = (IDomainEvent)JsonSerializer.Deserialize(message.Payload, type)!;
                    await publisher.PublishAsync(domainEvent);

                    message.ProcessedOn = DateTime.UtcNow;
                }
                catch (Exception ex) {
                    message.Erorr = ex.Message;
                    _logger.LogInformation($"ProcessOutboxException : {ex.Message}", message.Id);
                }
               
            }
           await context.SaveChangesAsync(stoppingToken);
        }
    }
}
