
namespace Domain.Aggregate.Order
{
    public class OrderItemSnapshot
    {
        public Guid ProductId { get; }

        public int Quantity {  get; }
        
        public OrderItemSnapshot(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
