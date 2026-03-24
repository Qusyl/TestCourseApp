using Domain.Aggregate.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specification
{
    public class GetProductByIdSpecification : Specification<Product>
    {
        public GetProductByIdSpecification(Guid Id) {
            Filter = p => p.Id == Id;
        }
    }
}
