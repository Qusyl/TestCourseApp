

using Domain.Events;

namespace Application.Interface
{
    public interface IEventPublisher
    {
        Task PublishAsync(IDomainEvent domainEvent);
    }
}
