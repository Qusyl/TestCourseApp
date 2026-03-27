using Application.Interface;


namespace Application.Command.Cart
{
    public class AddToCartCommand 
    {
        public Guid ProductId;
       
        public int Quantity;
    }
}
