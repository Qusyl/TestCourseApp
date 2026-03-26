using Application.Command.Order;
using Application.Handler.Order;
using Microsoft.AspNetCore.Mvc;

namespace TestCourseApp.Controllers.Product
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderHandler _createOrderHandler;

        public OrderController(CreateOrderHandler createOrderHandler) {
        
        _createOrderHandler = createOrderHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var res = await _createOrderHandler.Handle(command);

            if (!res.IsSuccess)
            {
                return BadRequest(res.Error);
            }
            return Ok(res.Value);
        }
    }
}
