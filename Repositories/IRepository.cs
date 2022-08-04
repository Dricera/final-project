using backend.Models;

namespace backend.Repositories
{
    public interface IRepository
    {
        Task<UserModel> GetItemAsync(Guid id);
        Task<IEnumerable<UserModel>> GetItemsAsync();
        Task CreateItemAsync(UserModel item);
        Task UpdateItemAsync(UserModel item);
        Task DeleteItemAsync(Guid id);
    }
}