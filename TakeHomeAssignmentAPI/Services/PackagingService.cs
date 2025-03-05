using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using TakeHomeAssignmentAPI.Data;
using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Services
{
    public interface IPackagingService
    {
        Task<IEnumerable<Packaging>> GetAllPackagingsAsync();
        Task<Packaging> GetPackagingByIdAsync(int id);
        Task AddPackagingAsync(Packaging packaging);
        Task UpdatePackagingAsync(Packaging packaging);
        Task DeletePackagingAsync(int id);
        Task<Packaging> CreatePackageAsync(Packaging packaging);
    }

    public class PackagingService : IPackagingService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PackagingService> _logger;

        public PackagingService(ApplicationDbContext context, ILogger<PackagingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Packaging>> GetAllPackagingsAsync()
        {
            _logger.LogInformation("Fetching all packagings from database");
            return await _context.Packagings.ToListAsync();
        }

        public async Task<Packaging> GetPackagingByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching packaging with ID {Id}", id);
                var packaging = await _context.Packagings.FindAsync(id);
                if (packaging == null)
                {
                    _logger.LogWarning("Packaging with ID {Id} not found", id);
                    throw new KeyNotFoundException($"Packaging with ID {id} not found.");
                }
                return packaging;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching packaging with ID {Id}", id);
                throw;
            }
        }

        public async Task AddPackagingAsync(Packaging packaging)
        {
            try
            {
                _logger.LogInformation("Adding new packaging");
                await _context.Packagings.AddAsync(packaging);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully added new packaging");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new packaging");
                throw;
            }
        }

        public async Task UpdatePackagingAsync(Packaging packaging)
        {
            try
            {
                _logger.LogInformation("Updating packaging with ID {Id}", packaging.Id);
                _context.Packagings.Update(packaging);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully updated packaging with ID {Id}", packaging.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating packaging with ID {Id}", packaging.Id);
                throw;
            }
        }

        public async Task DeletePackagingAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting packaging with ID {Id}", id);
                var packaging = await _context.Packagings.FindAsync(id);
                if (packaging != null)
                {
                    _context.Packagings.Remove(packaging);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Successfully deleted packaging with ID {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Packaging with ID {Id} not found for deletion", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting packaging with ID {Id}", id);
                throw;
            }
        }

        public async Task<Packaging> CreatePackageAsync(Packaging packaging)
        {
            try
            {
                _logger.LogInformation("Creating new package");
                await _context.Packagings.AddAsync(packaging);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Successfully created package");
                return packaging;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating package");
                throw;
            }
        }
    }
}
