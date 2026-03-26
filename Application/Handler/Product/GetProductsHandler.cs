using Application.Command.Product;
using Application.Interface;
using Domain;
using Domain.Aggregate.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Product
{
    public class GetProductsHandler
    {
        private readonly IProductRepository _repository;

        public GetProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<Domain.Aggregate.Product.Product?>, ApplicationError>> Handle(GetAllProductCommand command)
        {
            var products = await _repository.GetAllAsync();

            if (!products.Any()) {
                return Result<List<Domain.Aggregate.Product.Product?>, ApplicationError>.Failure(ApplicationError.ProductNotFound);
            }
               
            return Result<List<Domain.Aggregate.Product.Product?>,ApplicationError>.Success(products);
            }

    }
}
