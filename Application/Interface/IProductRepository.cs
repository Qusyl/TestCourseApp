using Domain.Aggregate.Product;

namespace Application.Interface
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);

        Task<Product?> GetByIdAsync(Guid id);

        Task<List<Product?>> GetAllAsync();

        Task<List<Product?>> GetByIdAsync(List<Guid> id);


    }
}
