
using Domain.Events;
using Domain.Events.Order;

namespace Domain.Aggregate.Order
{
    public class Order : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }    
        public OrderStatus Status { get; private set; }
        private readonly List<IDomainEvent> _events = new();
        private readonly List<OrderItem> _items = new ();

        public IReadOnlyCollection<OrderItem> Items => _items;
        public IReadOnlyCollection<IDomainEvent> Events => _events;

        private Order() { }

        public static Result<Order, OrderError> Create(Guid userId,IEnumerable<OrderItemSnapshot> items)
        {
            if (items == null || !items.Any()) return Result<Order, OrderError>.Failure(OrderError.InvalidOrderItemError);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Status = OrderStatus.Pending,
                UserId = userId
            };

            foreach (var item in items) {
                var OrderItmRes = order.AddItem(item);
                if (!OrderItmRes.IsSuccess) return Result<Order, OrderError>.Failure(OrderError.InvalidOrderItemError);
            }
            order._events.Add(new OrderCreateEvent(order.Id, items.ToList()));
            return Result<Order, OrderError>.Success(order);
        }
        public void MarkPaid()
        {
            if (Status != OrderStatus.Pending) return;
            Status = OrderStatus.Paid;
        }
        public void Cancel() {
            Status = OrderStatus.Cancelled;
        }
        public Result<OrderError> AddItem(OrderItemSnapshot item)
        {
            var Item = OrderItem.Create(item.ProductId, item.Quantity);
            if (!Item.IsSuccess) return Result<OrderError>.Failure(OrderError.InvalidOrderItemError);
            _items.Add(Item.Value);
            return Result<OrderError>.Success;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
