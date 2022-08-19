using backend.Models;

namespace backend.Repositories
{
	public class InMemItemsRepository : IRepository
    {
        private readonly List<UserModel> items = new()
        {
            new UserModel { id = Guid.NewGuid(), name = "Admin", role="adminTest", CreatedDate = DateTimeOffset.UtcNow },
            new UserModel { id = Guid.NewGuid(), name = "Manager", role="managerTest", CreatedDate = DateTimeOffset.UtcNow },
            new UserModel { id = Guid.NewGuid(), name = "User", role="userTest" , CreatedDate = DateTimeOffset.UtcNow }
        };

        async Task<IEnumerable<UserModel>> IRepository.GetItemsAsync()
        {
            return await Task.FromResult(items);	
        }

        public async Task<UserModel> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

         async Task IRepository.CreateItemAsync(UserModel item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(UserModel item)
        {
            var index = items.FindIndex(existingItem => existingItem.id == item.id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }


	}
}