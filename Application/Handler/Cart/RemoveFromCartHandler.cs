using Application.Command.Cart;
using Application.Interface;
using Domain;


namespace Application.Handler.Cart
{
    public class RemoveFromCartHandler
    {
        private readonly ICartRepository _cartRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RemoveFromCartHandler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(RemoveFromCartCommand command)
        {
            

            var cart = await _cartRepository.GetByIdAsync(command.UserId);

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
