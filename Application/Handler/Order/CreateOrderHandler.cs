

using Application.Command.Order;
using Application.Interface;
using Domain;
using Domain.Aggregate.Order;

namespace Application.Handler.Order
{
    public class CreateOrderHandler 
    {
        private readonly IOrderRepository _repository;

        private readonly IProductRepository _productRepository;

        private readonly IUnitOfWork _unit;

        public CreateOrderHandler(IOrderRepository repository, IProductRepository productRepository, IUnitOfWork unit)
        {
            _repository = repository;
            _productRepository = productRepository;
            _unit = unit;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(CreateOrderCommand command)
        {
            

            if (command.Items.Count == 0) return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidOrderItem);

            var productIds = command.Items.Select(x => x.productId)
                .Distinct()
                .ToList();

            var products = await _productRepository.GetByIdAsync(productIds);

            if (products.Count != productIds.Count)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.ProductNotFound);
            }

            var productDict = products.ToDictionary(p => p.Id);
            foreach (var item in command.Items) {
                if (!productDict.TryGetValue(item.productId, out var product))
                {
                    return Result<Guid, ApplicationError>.Failure(ApplicationError.ProductNotFound);
                }
                var reserve = product.ReserveStock(item.Quantity);
                if (!reserve.IsSuccess)
                {
                    return Result<Guid, ApplicationError>.Failure(ApplicationError.NotEnoughtInStock);
                }
            } 

            var snapshot = command.Items.Select(x => new OrderItemSnapshot(x.productId, x.Quantity)).ToList();

            var orderRes = Domain.Aggregate.Order.Order.Create(command.UserId,snapshot);

            if (!orderRes.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidOrder);
            }

            var order = orderRes.Value;

            await _repository.AddAsync(order);

            var save = await _unit.SaveChangesAsync();

            if (!save.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.ConcurrencyConflict);
            }

            return Result<Guid, ApplicationError>.Success(order.Id);
        }
    }
}
