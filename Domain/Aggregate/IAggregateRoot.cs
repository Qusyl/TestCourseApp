

using Domain.Events;

namespace Domain.Aggregate
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
