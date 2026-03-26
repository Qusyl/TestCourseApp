using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Product
{
    public class GetProductByIdCommand
    {
        public Guid UserId;

        public Guid ProductId;
    }
}
