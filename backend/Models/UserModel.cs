namespace backend.Models;
public class UserModel
{
	public Guid id { get; init; }
	public string name { get; set; } = string.Empty;
	public string role { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	/* 	
		// AUTH BEGIN
		public byte[] PasswordHash { get; set; }
			public byte[] PasswordSalt { get; set; }
			public string RefreshToken { get; set; } = string.Empty;
			public DateTime TokenCreated { get; set; }
			public DateTime TokenExpires { get; set; }
		// AUTH END 
	*/
}