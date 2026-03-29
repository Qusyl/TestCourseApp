using Application.Command.Order;
using Application.Handler.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestCourseApp.Controllers.Order
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderHandler _createOrderHandler;
        private readonly GetAllOrderByIdHandler _getAllOrderByIdHandler;


        public OrderController(CreateOrderHandler createOrderHandler, GetAllOrderByIdHandler getAllOrderByIdHandler) {
        
        _createOrderHandler = createOrderHandler;
        _getAllOrderByIdHandler = getAllOrderByIdHandler;
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllOrderByIdCommand command)
        {
            var res = await _getAllOrderByIdHandler.Handle(command);

            if (!res.IsSuccess) {
                return BadRequest(res.Error);
            }
            return Ok(res.Value);
        }
    }
}
