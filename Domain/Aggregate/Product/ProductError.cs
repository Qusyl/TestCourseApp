using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregate.Product
{
    public  sealed class ProductError
    {
        string Message { get; }

        private ProductError(string message) => Message = message;

        public static ProductError InvalidProductName => new ProductError("Product_name_is_invalid");

        public static ProductError InvalidProductPrice => new ProductError("Product_price_is_invalid");

        public static ProductError InvalidProductStock => new ProductError("Product_stock_is_invalid");

    }
}
