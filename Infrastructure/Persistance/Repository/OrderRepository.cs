using Application.Interface;
using Domain;
using Domain.Aggregate.Order;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Result<Order, OrderError> order)
        {
            if (order.IsSuccess)
            {
                var orderForAdd = order.Value;

                await _context.Orders.AddAsync(orderForAdd);

                await _context.SaveChangesAsync();
            }

        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<List<Order>> GetAllByIdAsync(Guid userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}
