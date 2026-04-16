using Application.Command.Product;
using Application.Interface;
using Domain;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Application.Handler.Product
{
    public class CreateProductHandler
    {
        private readonly IProductRepository _repository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMemoryCache _memoryCache;

        private readonly ILogger<CreateProductHandler> _logger;



        public CreateProductHandler(IProductRepository repository, IUnitOfWork unitOfWork, IMemoryCache memoryCache, ILogger<CreateProductHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<Result<Guid, ApplicationError>> Handle(CreateProductCommand command)
        {
            if (ContainsHtmlTags(command.Name))
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidCommand);
            }
            var res = Domain.Aggregate.Product.Product.Create(command.Name, command.Price, command.Stock);

            if(!res.IsSuccess) return Result<Guid, ApplicationError>.Failure(ApplicationError.InvalidProduct);

            var product = res.Value;

            await _repository.AddAsync(product);

          var save = await _unitOfWork.SaveChangesAsync();

            if (!save.IsSuccess)
            {
                return Result<Guid, ApplicationError>.Failure(ApplicationError.ConcurrencyConflict);
            }

            return Result<Guid, ApplicationError>.Success(product.Id);
        }
        private bool ContainsHtmlTags(string input)
        {
            return Regex.IsMatch(input, @"<[^>]*>", RegexOptions.IgnoreCase);
        }

        private string SanitizeHtml(string input)
        {
            return Regex.Replace(input, @"<[^>]*>", string.Empty);
        }
    }
}
