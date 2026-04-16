using Application.Command.Cart;
using Application.Interface;
using Domain;
using Microsoft.Extensions.Caching.Memory;


namespace Application.Handler.Cart
{
    public class AddToCartHandler
    {
        private readonly ICartRepository _repos;
        private readonly IUnitOfWork _unit;
        private readonly ICurrentUserService _currentUser;
        private readonly IMemoryCache _memoryCache;
        public AddToCartHandler(ICartRepository repos, IUnitOfWork unit, ICurrentUserService currentUser, IMemoryCache memoryCache)
        {
            _repos = repos;
            _unit = unit;
            _currentUser = currentUser;
            _memoryCache = memoryCache;
        }
        public async Task<Result<Guid, ApplicationError>> Handle(AddToCartCommand command)
        {

            var findUser = _currentUser.GetUserId();
            if (!findUser.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.NotAuthorized);
            }
            var userId = findUser.Value;
            var cart = await _repos.GetByIdAsync(userId);

            if(cart == null)
            {
                var cartResCreate = Domain.Aggregate.Cart.Cart.Create(userId);
                if (!cartResCreate.IsSuccess)
                {
                   return Result<Guid, ApplicationError>.Failure(ApplicationError.CartNotFound);
                }
                cart = cartResCreate.Value;
                await _repos.AddAsync(cart);
            }

           var addRes = cart.AddItem(command.ProductId, command.Quantity);

            if(!addRes.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidCartData);
            }

            var save = await _unit.SaveChangesAsync();
            if (!save.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.ConcurrencyConflict);
            }
            _memoryCache.Remove("CartCache");

            return Result<Guid, ApplicationError>.Success(cart.Id);
        }
    }
}
