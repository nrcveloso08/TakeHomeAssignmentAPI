using Microsoft.EntityFrameworkCore;
using Serilog;
using TakeHomeAssignmentAPI.DTO;
using TakeHomeAssignmentAPI.Models;
using TakeHomeAssignmentAPI.Repositories;

namespace TakeHomeAssignmentAPI.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<ProductWithPackagingDto>> GetProductsWithPackagingAsync();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly Serilog.ILogger _logger;        

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = Log.ForContext<ProductService>();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                _logger.Information("Fetching all products from the database.");
                var products = await _productRepository.GetAllProductsAsync();
                _logger.Information("Successfully retrieved {Count} products.", products.Count);
                return products;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while fetching products.");
                throw;
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                _logger.Information("Adding a new product: {@Product}", product);
                var newProduct = await _productRepository.AddProductAsync(product);
                _logger.Information("Successfully added product with ID {ProductId}.", newProduct.Id);
                return newProduct;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding a new product.");
                throw;
            }
        }

        public async Task<IEnumerable<ProductWithPackagingDto>> GetProductsWithPackagingAsync()
        {
            return await _productRepository.GetProductsWithPackagingAsync();
        }

    }
}
