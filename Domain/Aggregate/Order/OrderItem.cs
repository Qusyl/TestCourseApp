

namespace Domain.Aggregate.Order
{
    public class OrderItem
    {
        public Guid Id { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        private OrderItem(Guid productId,  int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
            Id = Guid.NewGuid();

        }
        private OrderItem() { }

        public static Result<OrderItem, OrderItemError> Create(Guid productId, int quantity)
        {
            if (productId == Guid.Empty) return Result<OrderItem, OrderItemError>.Failure(OrderItemError.InvalidProductId);

            if(quantity <= 0) return Result<OrderItem ,OrderItemError>.Failure(OrderItemError.InvalidQuantity);

            return Result<OrderItem, OrderItemError>.Success(new OrderItem(productId, quantity));
        }
    }
}
