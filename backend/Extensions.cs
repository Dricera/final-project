using backend.Dtos;
using backend.Models;

namespace backend
{

	public static class Extensions
	{
		/* 
         public static UserDto AsDto(this UserModel user)
        {
            return new UserDto(user.id, user.name,user.role, user.CreatedDate);
        } 
        DELETED FOR NOW
        */
		public static TicketDto ToDto(this TicketModel ticket)
		{
			return new TicketDto(ticket.ID, ticket.Subject, ticket.Description, ticket.CreatedDate, ticket.UpdatedDate, ticket.CompletedDate, ticket.Priority.ToString(), ticket.Status.ToString());
			// Display key ticket params in OpenAPI console
		}

	}
}