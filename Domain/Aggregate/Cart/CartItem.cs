

namespace Domain.Aggregate.Cart
{
    public class CartItem
    {
        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        private CartItem() { }
        private CartItem(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public static Result<CartItem, CartError> Create(Guid id, int quantity)
        {
            if(id == Guid.Empty)
            {
                return Result<CartItem, CartError>.Failure(CartError.NotExistingProduct);
            }
            if(quantity <= 0)
            {
                return Result<CartItem, CartError>.Failure(CartError.InvalidQuantity);
            }

            return Result<CartItem, CartError>.Success(new CartItem(id, quantity));
        }

        public void Increase(int quantity)
        {
            Quantity += quantity;
        }
    }
}
