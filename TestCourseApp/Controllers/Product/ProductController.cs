using Application.Command.Product;
using Application.Handler.Product;
using Microsoft.AspNetCore.Mvc;

namespace TestCourseApp.Controllers.Product
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductHandler _createProductHandler;

        private readonly GetProductByIdHandler _getProductByIdHandler;

        private readonly GetProductsHandler _getProductsHandler;

        public ProductController(CreateProductHandler createProductHandler, GetProductByIdHandler getProductByIdHandler, GetProductsHandler getProductsHandler)
        {
            _createProductHandler = createProductHandler;
            _getProductByIdHandler = getProductByIdHandler;
            _getProductsHandler = getProductsHandler;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _createProductHandler.Handle(command);

            if (!res.IsSuccess)
            {
                return BadRequest(res.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = res.Value }, res.Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Aggregate.Product.Product>>> GetAll([FromQuery] GetAllProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var products = await _getProductsHandler.Handle(command);
            if (!products.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(products.Value);
        }

        [HttpGet("{command.UserId}")]

        public async Task<IActionResult> GetById(GetProductByIdCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _getProductByIdHandler.Handle(command);

            if(!product.IsSuccess) return BadRequest(product.Error);

            return Ok(product.Value);
        }

    }
}
