using Application.Interface;


namespace Application.Command.Product
{
    public class CreateProductCommand 
    {
        public string Name { get; set; }
        public int Stock { get; set; }

        public decimal Price { get; set; }   
    }
}
