using Microsoft.AspNetCore.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace TakeHomeAssignmentAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string? TwoFactorCode { get; set; } // Optional 2FA Code
        public string? TwoFactorRecoveryCode { get; set; } // Optional Recovery Code

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class RegisterUser
    {
        public required string Email { get; init; }         
        public required string Password { get; init; }
        public string? TwoFactorCode { get; set; } // Optional 2FA Code
        public string? TwoFactorRecoveryCode { get; set; } // Optional Recovery Code

    }
}
