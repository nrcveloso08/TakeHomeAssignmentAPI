using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TakeHomeAssignmentAPI.Data;
using TakeHomeAssignmentAPI.Models;
using Microsoft.Extensions.Logging; 

namespace TakeHomeAssignmentAPI.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(string email, string password, string twoFactorCode, string twoFactorRecoveryCode);
        Task<User?> ValidateUserAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserService> _logger; 

        public UserService(ApplicationDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateUserAsync(string email, string password, string twoFactorCode, string twoFactorRecoveryCode)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = email,
                PasswordHash = hashedPassword,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User {Email} created successfully.", email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user {Email}", email);
                return false;
            }
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            try
            {
                _logger.LogInformation("Attempting to validate user {Username}.", username);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    _logger.LogWarning("Failed login attempt for user {Username}", username);
                    return null;
                }

                _logger.LogInformation("User {Username} authenticated successfully.", username);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user {Username}", username);
                return null;
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            try
            {
                _logger.LogInformation("Checking existence of user {Username}.", username);

                var exists = await _context.Users.AnyAsync(u => u.Username == username);
                _logger.LogInformation("User existence check for {Username}: {Exists}", username, exists);
                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user existence for {Username}", username);
                return false;
            }
        }
    }
}
