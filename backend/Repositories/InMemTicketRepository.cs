using backend.Models;

namespace backend.Repositories
{
	public class InMemTicketRepository : TicketRepository
    {
        private readonly List<TicketModel> tickets = new()
        {
            new TicketModel { TicketId = Guid.NewGuid(), Title = "Ticket1", Description = "Ticket number one", CreatedDate = DateTimeOffset.UtcNow },
            new TicketModel { TicketId = Guid.NewGuid(), Title = "Ticket2", Description = "Ticket number two",CreatedDate = DateTimeOffset.UtcNow },
            new TicketModel { TicketId = Guid.NewGuid(), Title = "Ticket3",Description = "Ticket number three", CreatedDate = DateTimeOffset.UtcNow }
        };

        async Task<IEnumerable<TicketModel>> TicketRepository.GetTicketsAsync()
        {
            return await Task.FromResult(tickets);	
        }

        public async Task<TicketModel> GetTicketAsync(Guid TicketId)
        {
            var ticket = tickets.Where(ticket => ticket.TicketId == TicketId).SingleOrDefault();
            return await Task.FromResult(ticket);
        }

         async Task TicketRepository.CreateTicketAsync(TicketModel ticket)
        {
            tickets.Add(ticket);
            await Task.CompletedTask;
        }

        public async Task UpdateTicketAsync(TicketModel ticket)
        {
            var index = tickets.FindIndex(existingticket => existingticket.TicketId == ticket.TicketId);
            tickets[index] = ticket;
            await Task.CompletedTask;
        }

        public async Task DeleteTicketAsync(Guid TicketId)
        {
            var index = tickets.FindIndex(existingticket => existingticket.TicketId == TicketId);
            tickets.RemoveAt(index);
            await Task.CompletedTask;
        }


	}
}