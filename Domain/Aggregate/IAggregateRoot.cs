

namespace Domain.Aggregate
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> Children { get; }

        void ClearEvents();
    }
}
