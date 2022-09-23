using backend.Models;

namespace backend.Repositories
{
    public interface IRepository
    {
        Task<UserModel> GetUserAsync(Guid id);
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task CreateUserAsync(UserModel user);
        Task UpdateUserAsync(UserModel user);
        Task DeleteUserAsync(Guid id);
    }
}