


namespace Domain.Aggregate.Product
{
    public class Product 
    {
      public Guid Id { get; private set; }

      public string Name { get; private set; }  

      public decimal Price { get; private set; }

      public int Stock { get; private set; }

      public uint Version { get; private set; }

     
        private Product(string name, decimal price, int stock)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Stock = stock;
        }
       
        public static Result<Product,ProductError> Create(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name)) return Result<Product, ProductError>.Failure(ProductError.InvalidProductName);
            if (price < 0) return Result<Product, ProductError>.Failure(ProductError.InvalidProductPrice);
            if(stock <= 0) return Result<Product, ProductError>.Failure(ProductError.InvalidProductStock);

            return Result<Product, ProductError>.Success(new Product(name, price, stock));
        }

        public  Result<ProductError> ReserveStock(int quantity)
        {
            if (quantity > Stock) return Result<ProductError>.Failure(ProductError.InvalidProductStock);
            Stock -= quantity;
            return Result<ProductError>.Success;
        }

        public Result<ProductError> ReleaseStock(int quantity)
        {
            if (quantity < 0) return Result<ProductError>.Failure(ProductError.InvalidProductStock);

            Stock -= quantity;

            return Result<ProductError>.Success;
        }
    }
}
