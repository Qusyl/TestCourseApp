using Application.Command.Order;
using Application.Interface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Order
{
    public class GetAllOrderByIdHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentUserService _currentUserService;



       public GetAllOrderByIdHandler(IOrderRepository orderRepository, ICurrentUserService currentUserService)
        {
            _orderRepository = orderRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<Domain.Aggregate.Order.Order>, ApplicationError>> Handle(GetAllOrderByIdCommand command)
        {
            var findUser = _currentUserService.GetUserId();
            if (!findUser.IsSuccess) {
                return Result<List<Domain.Aggregate.Order.Order>, ApplicationError>.Failure(ApplicationError.NotAuthorized);
            }
            var res = await _orderRepository.GetAllByIdAsync(findUser.Value);

            if(res.Count == 0)
            {
                return Result<List<Domain.Aggregate.Order.Order>, ApplicationError>.Failure(ApplicationError.OrderNotFound);
            }

            return Result<List<Domain.Aggregate.Order.Order>, ApplicationError>.Success(res);
        }
    }
}
