

namespace Domain
{
    public interface IDomainEvent
    {
        string EventType { get;  }

        int Version { get;  }

        DateTime OccurredOn {  get; }    
    }
}
