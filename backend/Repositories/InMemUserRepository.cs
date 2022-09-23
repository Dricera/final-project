using backend.Models;

namespace backend.Repositories
{
	public class InMemUserRepository : IRepository
    {
        private readonly List<UserModel> users = new()
        {
            new UserModel { id = Guid.NewGuid(), name = "Admin", role="adminTest", CreatedDate = DateTimeOffset.UtcNow },
            new UserModel { id = Guid.NewGuid(), name = "Manager", role="managerTest", CreatedDate = DateTimeOffset.UtcNow },
            new UserModel { id = Guid.NewGuid(), name = "User", role="userTest" , CreatedDate = DateTimeOffset.UtcNow }
        };

        async Task<IEnumerable<UserModel>> IRepository.GetUsersAsync()
        {
            return await Task.FromResult(users);	
        }

        public async Task<UserModel> GetUserAsync(Guid id)
        {
            var user = users.Where(user => user.id == id).SingleOrDefault();
            return await Task.FromResult(user);
        }

         async Task IRepository.CreateUserAsync(UserModel user)
        {
            users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            var index = users.FindIndex(existingUser => existingUser.id == user.id);
            users[index] = user;
            await Task.CompletedTask;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var index = users.FindIndex(existingUser => existingUser.id == id);
            users.RemoveAt(index);
            await Task.CompletedTask;
        }


	}
}