using Application.Command.Cart;
using Application.Interface;
using Domain;
using Domain.Aggregate.Order;


namespace Application.Handler.Cart
{
    public class CheckOutCartHandler
    {
        private ICartRepository _repos;

        public CheckOutCartHandler(ICartRepository cartRepository)
        {
            _repos = cartRepository;
        }
        public async Task<Result<Guid, ApplicationError>> Handle(CheckOutCartCommand command)
        {
           
            var cart = await _repos.GetByIdAsync(command.ProductId);

            if (cart == null) {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.CartNotFound);
            }

           if(cart.Items.Count <= 0)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidCartData);
            }

            var snapshot = cart.Items.Select(c => new OrderItemSnapshot(c.ProductId, c.Quantity)).ToList();

            var orderRes = Domain.Aggregate.Order.Order.Create(cart.UserId, snapshot);

            cart.ClearEvents();

            return Result<Guid, ApplicationError>.Success(orderRes.Value.Id);
        }
    }
}
