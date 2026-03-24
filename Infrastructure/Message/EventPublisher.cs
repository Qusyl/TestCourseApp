using Application.Interface;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Message
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> _logger;

        public EventPublisher(ILogger<EventPublisher> logger)
        {
            _logger = logger;
        }



        public Task PublishAsync(IDomainEvent domainEvent)
        {
            _logger.LogInformation($"Publish event : {domainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
