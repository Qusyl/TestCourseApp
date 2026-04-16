using Application.Command.Product;
using Application.Interface;
using Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Application.Handler.Product
{
    public class GetProductsHandler
    {
        private readonly IProductRepository _repository;
        private readonly IMemoryCache _memoryCache;

        private readonly ILogger<GetProductsHandler> _logger;

        private const string CreateProductCache = "Products";

        public GetProductsHandler(IProductRepository repository, IMemoryCache memoryCache, ILogger<GetProductsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<Result<List<Domain.Aggregate.Product.Product?>, ApplicationError>> Handle(GetAllProductCommand command)
        {
            if (_memoryCache.TryGetValue(CreateProductCache, out  List<Domain.Aggregate.Product.Product?> cached))
            {
                _logger.LogInformation("Данные взяты из кэша");
                return Result<List<Domain.Aggregate.Product.Product?>, ApplicationError>.Success(cached);
            }
            _logger.LogWarning("Кэш пуст, идет обращение к БД...");
            var products = await _repository.GetAllAsync();
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                .SetPriority(CacheItemPriority.High)
                .SetSize(1);
            if (!products.Any())
            {
                _logger.LogInformation("Продуктов в БД нет, кэшируем пустой список");
                var emptyList = new List<Domain.Aggregate.Product.Product?>();
                _memoryCache.Set(CreateProductCache, emptyList, cacheOptions);
                return Result<List<Domain.Aggregate.Product.Product?>, ApplicationError>.Success(products);
            }
            
            _memoryCache.Set(CreateProductCache,products,cacheOptions);
            _logger.LogInformation($"products выгружены из БД и сохранены в кэш");
            return Result<List<Domain.Aggregate.Product.Product?>,ApplicationError>.Success(products);
            }

    }
}
