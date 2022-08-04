namespace backend.Models
{
	public class UserModel
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string role { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
	}

}
