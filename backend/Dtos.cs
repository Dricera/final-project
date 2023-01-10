using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
	// public record UserDto(Guid id, string name, DateTimeOffset CreatedDate);

	/* public record UserDto(Guid Id, string name, string role, DateTimeOffset CreatedDate);
    public record CreateUserDto([Required] string name, string role);
    public record UpdateUserDto([Required] string name, string role ); */

	public record TicketDto(Guid ID, string Subject, string Description, DateTimeOffset CreatedDate, DateTimeOffset UpdatedDate, DateTimeOffset CompletedDate, string priority, string status);

	public record CreateTicketDto([Required] string Subject, string Description);
	public record UpdateTicketDto([Required] Guid ID, string Subject, string Description);
}