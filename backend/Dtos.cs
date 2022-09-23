using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
	// public record UserDto(Guid id, string name, DateTimeOffset CreatedDate);
	public record UserDto(Guid Id, string name, string role, DateTimeOffset CreatedDate);
    public record CreateUserDto([Required] string name, string role);
    public record UpdateUserDto([Required] string name, string role );

	public record TicketDto(Guid TicketId, string Title, string Description, DateTimeOffset CreatedDate);

	public record CreateTicketDto([Required] string Title, string Description);
    public record UpdateTicketDto([Required] string Title, string Description);
}