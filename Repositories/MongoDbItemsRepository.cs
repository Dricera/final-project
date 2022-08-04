using backend.Models;
using backend.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class MongoDbItemsRepository : IRepository
    {
        private const string databaseName = "backend";
        private const string collectionName = "users";
        private readonly IMongoCollection<UserModel> itemsCollection;
        private readonly FilterDefinitionBuilder<UserModel> filterBuilder = Builders<UserModel>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<UserModel>(collectionName);
        }

        public async Task CreateItemAsync(UserModel item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<UserModel> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserModel>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(UserModel item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.id, item.id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}