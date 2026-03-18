

namespace Domain.Aggregate.Order
{
    public sealed class OrderItemError
    {
        public string Message { get; }

        private OrderItemError(string message) => Message = message;

        public static OrderItemError InvalidProductId => new OrderItemError("product_id_is_null_or_incorrect");
        public static OrderItemError InvalidQuantity => new OrderItemError("quantity_is_negative_or_incorrect");
    }
}
