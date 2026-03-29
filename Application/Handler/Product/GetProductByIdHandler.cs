

using Application.Command.Product;
using Application.Interface;
using Domain;


namespace Application.Handler.Product
{
    public class GetProductByIdHandler
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Domain.Aggregate.Product.Product, ApplicationError>> Handle(GetProductByIdCommand command)
        {
            var res = await _repository.GetByIdAsync(command.ProductId);

            if (res == null) {
                return Result<Domain.Aggregate.Product.Product, ApplicationError>.Failure(ApplicationError.ProductNotFound);

           }

            return Result<Domain.Aggregate.Product.Product, ApplicationError>.Success(res);
        }
    }
}
