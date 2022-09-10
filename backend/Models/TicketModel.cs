namespace backend.Models
{
	public class TicketModel
	{
		private DateTimeOffset createdDate;

		public Guid TicketId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTimeOffset CreatedDate { get => createdDate; set => createdDate = DateTimeOffset.UtcNow; }
		public DateTimeOffset UpdatedDate { get; set; }
		public DateTimeOffset CompletedDate { get; set; }

	}
}