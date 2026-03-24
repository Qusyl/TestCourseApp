using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.Product
{
    public class CreateProductCommand : IEntityCommand
    {
        public string Name { get; }
        public int Stock { get; }

        public decimal Price { get; }   
    }
}
