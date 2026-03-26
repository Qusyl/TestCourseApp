

using Domain.Events;

namespace Domain.Aggregate.Cart
{
    public class Cart : IAggregateRoot
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set;} 
        private List<IDomainEvent> _events = new();

        private List<CartItem> _items = new();
        public IReadOnlyCollection<IDomainEvent> Events => _events;

        public IReadOnlyCollection<CartItem> Items => _items;

        private Cart() { }  

        public static Result<Cart,CartError> Create(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return Result<Cart, CartError>.Failure(CartError.InvalidUser);
            }

            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId 
            };
            return Result<Cart, CartError>.Success(cart);
        }

        public Result<CartError> AddItem(Guid productId, int quantity)
        {
            if (quantity <= 0)
                return Result<CartError>.Failure(CartError.InvalidQuantity);

            var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Increase(quantity);
                return Result<CartError>.Success;
            }

            var itemResult = CartItem.Create(productId, quantity);

            if (!itemResult.IsSuccess)
                return Result<CartError>.Failure(itemResult.Error);

            _items.Add(itemResult.Value);

            return Result<CartError>.Success;
        }

        public void RemoveItem(Guid productId)
        {
            _items.RemoveAll(x => x.ProductId == productId);
        }

        public void ClearEvents()
        {
         _events.Clear();
        }
    }
}
