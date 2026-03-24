using Application.Interface;
using Domain;
using Domain.Aggregate.Order;

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
    }
}
