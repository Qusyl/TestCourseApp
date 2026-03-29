using Application.Interface;


namespace Application.Command.Cart
{
    public class AddToCartCommand 
    {
        public Guid ProductId { get; set; }
       
        public int Quantity { get; set; }
    }
}
