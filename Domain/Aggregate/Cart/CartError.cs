
namespace Domain.Aggregate.Cart
{
    public class CartError
    {
        public string Message { get;  }

        public string Code { get; }

        private CartError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public static CartError NotExistingProduct => new CartError("Product not existing", "product_not_exists");
        public static CartError InvalidQuantity => new CartError("Invalid product quantity", "invalid_quantity_error");
        public static CartError InvalidUser => new CartError("Invalid product quantity", "invalid_quantity_error");


    }
}
