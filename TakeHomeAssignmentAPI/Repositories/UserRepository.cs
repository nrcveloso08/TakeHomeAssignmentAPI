using TakeHomeAssignmentAPI.Models;

namespace TakeHomeAssignmentAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
    }

}
