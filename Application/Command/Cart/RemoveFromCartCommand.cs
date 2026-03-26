using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Cart
{
    public class RemoveFromCartCommand 
    {
        public Guid ProductId;

        public Guid UserId;
    }
}
