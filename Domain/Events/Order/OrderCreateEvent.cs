using Domain.Aggregate.Order;


namespace Domain.Events.Order
{
    public class OrderCreateEvent : IDomainEvent
    {
        public Guid OrderId { get; }

        public DateTime OccurredOn { get; }

        public IReadOnlyCollection<OrderItemSnapshot> Items { get; }
        public string EventType => "Order.created";

        public int Version => 1;

       public OrderCreateEvent(Guid orderId, IReadOnlyCollection<OrderItemSnapshot> items)
        {
            OrderId = orderId; Items = items;
            OccurredOn = DateTime.Now;
        }
    }
}
