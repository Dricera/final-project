using backend.Models;

namespace backend.Repositories
{
	public class InMemTicketRepository : TicketRepository
    {
        private readonly List<TicketModel> tickets = new()
        {
            new TicketModel { id = Guid.NewGuid(), Subject = "Ticket1", Description = "Ticket number one"},
            new TicketModel { id = Guid.NewGuid(), Subject = "Ticket2", Description = "Ticket number two"},
            new TicketModel { id = Guid.NewGuid(), Subject = "Ticket3",Description = "Ticket number three"}
        };

        async Task<IEnumerable<TicketModel>> TicketRepository.GetTicketsAsync()
        {
            return await Task.FromResult(tickets);	
        }

        public async Task<TicketModel> GetTicketAsync(Guid ID)
        {
            var ticket = tickets.Where(ticket => ticket.id == ID).SingleOrDefault();
            return await Task.FromResult(ticket);
        }

         async Task TicketRepository.CreateTicketAsync(TicketModel ticket)
        {
            tickets.Add(ticket);
            await Task.CompletedTask;
        }

        public async Task UpdateTicketAsync(TicketModel ticket)
        {
            var index = tickets.FindIndex(existingticket => existingticket.id == ticket.id);
            tickets[index] = ticket;
            await Task.CompletedTask;
        }

        public async Task UpdateTicketAsync(Guid id, TicketModel ticket)
		{
			var index = tickets.FindIndex(existingticket => existingticket.id == id);
			var foundTicket = tickets[index];
			
			foundTicket.Subject = ticket.Subject ?? foundTicket.Subject;
            foundTicket.Description = ticket.Description ?? foundTicket.Description;

            if(ticket.Priority.ToString().Length > 0)
            foundTicket.Priority = ticket.Priority;

			if (!foundTicket.Status.Equals("Completed"))
			{
				foundTicket.Status = ticket.Status;
			}
			// SetMatchingProperties(ticket, foundTicket);

			await Task.CompletedTask;
		}

        public async Task DeleteTicketAsync(Guid ID)
        {
            var index = tickets.FindIndex(existingticket => existingticket.id == ID);
            tickets.RemoveAt(index);
            await Task.CompletedTask;
        }

        private static void SetMatchingProperties(object source, object destination)
		{
			var sourceProperties = source.GetType().GetProperties();
			var destinationProperties = destination.GetType().GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
				if (destinationProperty == null || !destinationProperty.CanWrite)
				{
					continue;
				}

				var value = sourceProperty.GetValue(source);
				if (!IsDefaultValue(value))
				{
					destinationProperty.SetValue(destination, value);
				}
			}
		}
		private static bool IsDefaultValue(object value)
		{
			if (value == null)
			{
				return true;
			}

			Type type = value.GetType();
			if (type.IsValueType)
			{
				return value.Equals(Activator.CreateInstance(type));
			}

			return false;
		}


	}
}