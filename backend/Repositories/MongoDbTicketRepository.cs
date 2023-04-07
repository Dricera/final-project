using backend.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace backend.Repositories;

public class MongoDbTicketRepository: TicketRepository
{

private const string databaseName = "TicketDB";
private const string collectionName = "Tickets";
private readonly IMongoCollection<TicketModel> ticketsCollection;
private readonly FilterDefinitionBuilder<TicketModel> filterBuilder = Builders<TicketModel>.Filter;

	public MongoDbTicketRepository(IMongoClient mongoClient)
	{
		IMongoDatabase database = mongoClient.GetDatabase(databaseName);
		ticketsCollection = database.GetCollection<TicketModel>(collectionName);
	}

	public async Task<IEnumerable<TicketModel>> GetTicketsAsync()
	{
		// return await ticketsCollection.Find(filterBuilder.Empty).ToListAsync();
		return await ticketsCollection.Find(new BsonDocument()).ToListAsync();
	}

	public async Task<TicketModel> GetTicketAsync(Guid ID)
	{
		var filter = filterBuilder.Eq(ticket => ticket.id, ID);
		return await ticketsCollection.Find(filter).SingleOrDefaultAsync();
	}

	public async Task CreateTicketAsync(TicketModel ticket)
	{
		await ticketsCollection.InsertOneAsync(ticket);
	}

	public async Task DeleteTicketAsync(Guid ID)
	{
		var filter = filterBuilder.Eq(ticket => ticket.id, ID);
		await ticketsCollection.DeleteOneAsync(filter);
	}

	public async Task UpdateTicketAsync(Guid ID,TicketModel ticket)
	{
		var filter = filterBuilder.Eq(existingTicket => existingTicket.id, ID);
		await ticketsCollection.ReplaceOneAsync(filter, ticket);
	}

}