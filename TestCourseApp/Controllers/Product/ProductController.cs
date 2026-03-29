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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            
            var res = await _createProductHandler.Handle(command);

            if (!res.IsSuccess)
            {
                return BadRequest(res.Error);
            }

            return Created($"api/products/{res.Value}", res.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var products = await _getProductsHandler.Handle(new GetAllProductCommand());
            if (!products.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(products.Value);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetProductByIdCommand { ProductId = id};
            var product = await _getProductByIdHandler.Handle(command);

            if(!product.IsSuccess) return BadRequest(product.Error);

            return Ok(product.Value);
        }

    }
}
