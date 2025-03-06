using Microsoft.EntityFrameworkCore;
using System.Data;
using TakeHomeAssignmentAPI.Data; 
using TakeHomeAssignmentAPI.DTO;
using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<ProductWithPackagingDto>> GetProductsWithPackagingAsync();
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<ProductWithPackagingDto>> GetProductsWithPackagingAsync()
        {
            var result = new List<ProductWithPackagingDto>();

            using (var connection = _context.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetPackagingHierarchy";
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var product = new ProductWithPackagingDto
                            {
                                ProductId = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : (int?)null,
                                Name = reader["ProductName"]?.ToString(),
                                Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : (decimal?)null,
                                PackagingId = Convert.ToInt32(reader["PackagingID"]),
                                Type = reader["Type"]?.ToString(),
                                Dimensions = reader["Dimensions"]?.ToString(),
                                Weight = reader["Weight"] != DBNull.Value ? Convert.ToDouble(reader["Weight"]) : 0,
                                ParentPackagingId = reader["ParentPackagingID"] != DBNull.Value ? Convert.ToInt32(reader["ParentPackagingID"]) : (int?)null
                            };

                            result.Add(product);
                        }
                    }
                }
            }

            return result;
        }

    }
}
