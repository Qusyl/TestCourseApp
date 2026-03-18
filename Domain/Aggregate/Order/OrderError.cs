


namespace Domain.Aggregate.Order
{
    public class OrderError
    {
        public string Message { get; }

        private OrderError(string message) => Message = message;

        public static OrderError InvalidOrderItemError => new OrderError("order_items_are_incorrect");


    }
}
