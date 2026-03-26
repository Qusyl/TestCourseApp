using Application.Command.Cart;
using Application.Interface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Cart
{
    public class AddToCartHandler
    {
        private readonly ICartRepository _repos;
        private readonly IUnitOfWork _unit;
        public AddToCartHandler(ICartRepository repos, IUnitOfWork unit)
        {
            _repos = repos;
            _unit = unit;
        }
        public async Task<Result<Guid, ApplicationError>> Handle(AddToCartCommand command)
        {
  
            var cart = await _repos.GetByIdAsync(command.UserId);

            if(cart == null)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.CartNotFound);
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

            return Result<Guid, ApplicationError>.Success(cart.Id);
        }
    }
}
