using Application.Command.Product;
using Application.Handler.Product;
using Domain.Aggregate.Product;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public ProductController(CreateProductHandler createProductHandler)
        {
            _createProductHandler = createProductHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var res = await _createProductHandler.Handle(command);

            if (!res.IsSuccess)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Aggregate.Product.Product>>> GetAll(GetAllProductCommand command)
        {
           var products = await _getProductsHandler.Handle(command);
            if (!products.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(products.Value);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(GetProductByIdCommand command)
        {
            var product = await _getProductByIdHandler.Handle(command);

            if(!product.IsSuccess) return BadRequest(product.Error);

            return Ok(product.Value);
        }

    }
}
