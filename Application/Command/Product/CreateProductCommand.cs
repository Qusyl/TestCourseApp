using Application.Interface;


namespace Application.Command.Product
{
    public class CreateProductCommand 
    {
        public string Name { get; }
        public int Stock { get; }

        public decimal Price { get; }   
    }
}
