using Domain.Aggregate.Cart;


namespace Application.Interface
{
    public interface ICartRepository
    {
        Task<Cart?> GetByIdAsync(Guid userId);

        Task AddAsync(Cart cart);

     }
}
