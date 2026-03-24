using Domain.Aggregate.Product;


namespace Application.Specification
{
    public class GetProductByNameSpecification : Specification<Product>
    {
        public GetProductByNameSpecification(string name) {
        Filter = p => p.Name == name;   
        }
    }
}
