using Application.Command.Cart;
using Application.Interface;
using Domain;


namespace Application.Handler.Cart
{
    public class RemoveFromCartHandler
    {
        private readonly ICartRepository _cartRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ICurrentUserService _currentUser;

        public RemoveFromCartHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(RemoveFromCartCommand command)
        {
            var findUser = _currentUser.GetUserId();
            if (!findUser.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.NotAuthorized);
            }
            var userId = findUser.Value;

            var cart = await _cartRepository.GetByIdAsync(userId);

            if(cart == null)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.CartNotFound);
            }

            cart.RemoveItem(command.ProductId);

            var save = await _unitOfWork.SaveChangesAsync();

            if (!save.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.ConcurrencyConflict);
            }
            return Result<Guid, ApplicationError>.Success(cart.Id);
        }
    }
}
