using backend.Models;
using backend.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class MongoDbUsersRepository : IRepository
    {
        private const string databaseName = "backend";
        private const string collectionName = "users";
        private readonly IMongoCollection<UserModel> usersCollection;
        private readonly FilterDefinitionBuilder<UserModel> filterBuilder = Builders<UserModel>.Filter;

        public MongoDbUsersRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            usersCollection = database.GetCollection<UserModel>(collectionName);
        }

        public async Task CreateUserAsync(UserModel user)
        {
            await usersCollection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.id, id);
            await usersCollection.DeleteOneAsync(filter);
        }

        public async Task<UserModel> GetUserAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.id, id);
            return await usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return await usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            var filter = filterBuilder.Eq(existingUser => existingUser.id, user.id);
            await usersCollection.ReplaceOneAsync(filter, user);
        }
    }
}