using backend.Dtos;
using backend.Models;

namespace backend{

    public static class Extensions
    {
        public static ItemDto AsDto(this UserModel item)
        {
            return new ItemDto(item.id, item.name,item.role, item.CreatedDate);
        }
        public static TicketDto ToDto(this TicketModel ticket)
        {
            return new TicketDto(ticket.TicketId, ticket.Title, ticket.Description, ticket.CreatedDate);
        }

    }
}