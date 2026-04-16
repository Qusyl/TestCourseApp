

namespace Application
{
    public class ApplicationError
    {
        public string Message { get;  }

        private ApplicationError(
            
            string message
            ) => Message = message;

        public static ApplicationError InvalidProduct => new ApplicationError("Product is null or invalid");
        public static ApplicationError InvalidUserData => new ApplicationError("User is invalid");
        public static ApplicationError InvalidCartData => new ApplicationError("Cart item is invalid");
        public static ApplicationError InvalidOrder => new ApplicationError("Order is null or invalid");
        public static ApplicationError EmptyOrder => new ApplicationError("Order is empty");
        public static ApplicationError EmptyProduct => new ApplicationError("Product is empty");
        public static ApplicationError InvalidOrderItem => new ApplicationError("Invalid order item");
        public static ApplicationError ConcurrencyConflict => new ApplicationError("Concurrency conflict error");
        public static ApplicationError NotEnoughtInStock => new ApplicationError("Not enough product in stock");
        public static ApplicationError ProductNotFound => new ApplicationError("Product is not found");
        public static ApplicationError OrderNotFound => new ApplicationError("Order is not found");
        public static ApplicationError CartNotFound => new ApplicationError("Cart is not found");
        public static ApplicationError NotAuthorized => new ApplicationError("Cart is not found");
        public static ApplicationError InvalidCommand => new ApplicationError("Invalid Command");

       

        public static ApplicationError InvalidCredentials => new ApplicationError("Invalid Credentials for logging");

        public static ApplicationError CommandCastError => new ApplicationError("Command cast exception");
    }
}
