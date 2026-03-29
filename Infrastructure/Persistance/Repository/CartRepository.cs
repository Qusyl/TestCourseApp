using Application.Interface;
using Domain.Aggregate.Cart;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetByIdAsync(Guid userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
