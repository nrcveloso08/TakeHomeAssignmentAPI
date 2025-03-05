using Microsoft.EntityFrameworkCore;
using TakeHomeAssignmentAPI.Data;
using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Repositories
{
    public interface IPackagingRepository
    {
        Task<List<Packaging>> GetAllPackagingsAsync();
        Task<Packaging?> GetPackagingByIdAsync(int id);
        Task<Packaging> AddPackagingAsync(Packaging packaging);
        Task<bool> DeletePackagingAsync(int id);
    }

    public class PackagingRepository: IPackagingRepository
    {
        private readonly ApplicationDbContext _context;

        public PackagingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Packaging>> GetAllPackagingsAsync()
        {
            return await _context.Packagings.ToListAsync();
        }

        public async Task<Packaging?> GetPackagingByIdAsync(int id)
        {
            return await _context.Packagings.FindAsync(id);
        }

        public async Task<Packaging> AddPackagingAsync(Packaging packaging)
        {
            _context.Packagings.Add(packaging);
            await _context.SaveChangesAsync();
            return packaging;
        }

        public async Task<bool> DeletePackagingAsync(int id)
        {
            var packaging = await _context.Packagings.FindAsync(id);
            if (packaging == null) return false;

            _context.Packagings.Remove(packaging);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
