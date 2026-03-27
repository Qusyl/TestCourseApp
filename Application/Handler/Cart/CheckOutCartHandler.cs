using Application.Command.Cart;
using Application.Interface;
using Domain;
using Domain.Aggregate.Order;


namespace Application.Handler.Cart
{
    public class CheckOutCartHandler
    {
        private readonly ICartRepository _repos;
        private readonly ICurrentUserService _currentUser;
        public CheckOutCartHandler(ICartRepository cartRepository, ICurrentUserService currentUser)
        {
            _repos = cartRepository;
            _currentUser = currentUser;
        }
        public async Task<Result<Guid, ApplicationError>> Handle(CheckOutCartCommand command)
        {
            var findUser = _currentUser.GetUserId();
            if (!findUser.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.NotAuthorized);
            }
            var userId = findUser.Value;
            var cart = await _repos.GetByIdAsync(userId);

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
