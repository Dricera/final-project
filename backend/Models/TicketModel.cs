namespace backend.Models
{
	public class TicketModel
	{
		private DateTimeOffset createdDate;
		private DateTimeOffset updatedDate;
		private DateTimeOffset completedDate;
		public Guid ID { get; set; }
		/* public List<TicketUser> Collaborators { get; set; }
		public TicketUser Assignee { get; set; } */
		public string Subject { get; set; }
		public string Description { get; set; }
		public DateTimeOffset CreatedDate { get => createdDate; set => createdDate = DateTimeOffset.UtcNow; }
		public DateTimeOffset UpdatedDate { get => updatedDate; set => updatedDate = DateTimeOffset.UtcNow; }
		public DateTimeOffset CompletedDate { get => completedDate; set => completedDate = DateTimeOffset.UtcNow; }

		public enum TicketStatus
		{
			Open,
			InProgress,
			Closed
		}
		private TicketStatus status;
		public TicketStatus Status
		{
			get => status;
			set
			{
				status = value;
				if (status == TicketStatus.Closed)
				{
					CompletedDate = DateTimeOffset.UtcNow;
					Console.WriteLine("Closed Ticket");
				}
				else
				{
					CompletedDate = default;
				}
				UpdatedDate = DateTimeOffset.UtcNow;
			}
		}
		public enum TicketPriority
		{
			Low = 0,
			Medium = 1,
			High = 2,
			Critical = 3
		}
		private TicketPriority priority;
		public TicketPriority Priority
		{
			get { return priority; }
			set
			{
				priority = value;
				UpdatedDate = DateTimeOffset.UtcNow;
			}
		}

		public TicketModel()
		{
			CreatedDate = DateTime.UtcNow;
			Status = TicketStatus.Open;
		}
	}
}