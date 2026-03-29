using Domain;
using Domain.Aggregate.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderRepository
    {
        Task AddAsync(Result<Order, OrderError> order);

        Task AddAsync(Order order);

        Task<List<Order>> GetAllByIdAsync(Guid userId);
    }
}
