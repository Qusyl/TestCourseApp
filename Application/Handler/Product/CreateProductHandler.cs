using Application.Command.Product;
using Application.Interface;
using Domain;
using Domain.Aggregate.Product;

namespace Application.Handler.Product
{
    public class CreateProductHandler : IHandler
    {
        private readonly IProductRepository _repository;

        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(IEntityCommand command)
        {
            var castCommand = command as CreateProductCommand;

            if (castCommand == null)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.CommandCastError);
            }

            var res = Domain.Aggregate.Product.Product.Create(castCommand.Name, castCommand.Price, castCommand.Stock);

            if(!res.IsSuccess) return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidProduct);

            var product = res.Value;

            await _repository.AddAsync(product);

          var save = await _unitOfWork.SaveChangesAsync();

            if (!save.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.ConcurrencyConflict);
            }

            return Result<Guid, ApplicationError>.Success(product.Id);
        }
    }
}
