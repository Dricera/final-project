namespace backend.Repositories;

using backend.Models;


    public interface TicketRepository
    {
        Task<TicketModel> GetTicketAsync(Guid id);
        Task<IEnumerable<TicketModel>> GetTicketsAsync();
        Task CreateTicketAsync(TicketModel ticket);
        Task UpdateTicketAsync(Guid id, TicketModel ticket);
        Task DeleteTicketAsync(Guid id);
    }
