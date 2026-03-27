using Application.Dto;
using Application.Interface;
using Domain.Aggregate.Order;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Command.Order
{
    public class CreateOrderCommand 
    {
        public List<OrderItemDto> Items { get; }
    }
}
