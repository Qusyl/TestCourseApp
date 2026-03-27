using Application.Command.Cart;
using Application.Handler.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestCourseApp.Controllers.Cart
{
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CheckOutCartHandler _checkOutCartHandler;

        private readonly AddToCartHandler _addToCartHandler;

        private readonly RemoveFromCartHandler _removeFromCartHandler;

        public CartController(CheckOutCartHandler checkOutCartHandler, AddToCartHandler addToCartHandler, RemoveFromCartHandler removeFromCartHandler)
        {
            _checkOutCartHandler = checkOutCartHandler;
            _addToCartHandler = addToCartHandler;
            _removeFromCartHandler = removeFromCartHandler;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut()
        {

            var command = new CheckOutCartCommand();
           
            var cart = await _checkOutCartHandler.Handle(command);

            if (!cart.IsSuccess)
            {
                return BadRequest(cart.Error);
            }

            return CreatedAtAction(nameof(CheckOut), new { id = cart.Value }, cart.Value);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
           
            var result = await _addToCartHandler.Handle(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);

            }

            return Ok(result.Value);
        }

        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> DeleteFromCart([FromBody] Guid productId)
        {
            
            var command = new RemoveFromCartCommand
            {
                ProductId = productId
            };

            var result = await _removeFromCartHandler.Handle(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}
