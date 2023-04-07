using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using backend.Dtos;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/ticket")]
[Produces("application/json")]
public class TicketController : ControllerBase
{
	private readonly TicketRepository _repository;
	private readonly ILogger<TicketController> _logger;

	public TicketController(TicketRepository repository, ILogger<TicketController> logger)
	{
		_repository = repository;
		_logger = logger;
	}


	[HttpGet]
	public async Task<IEnumerable<TicketDto>> GetTicketsAsync(string name = null)
	{
		var tickets = (await _repository.GetTicketsAsync())
					.Select(ticket => ticket.ToDto());

		// if (!string.IsNullOrWhiteSpace(name))
		if (name == "")
		{
			tickets = tickets.Where(ticket => ticket.Subject.Contains(name, StringComparison.OrdinalIgnoreCase));
		}
		_logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {tickets.Count()} tickets");

		return tickets;
	}

	[HttpGet("{id}", Name = "Ticket")]
	public async Task<ActionResult<TicketDto>> GetTicketAsync(Guid id)
	{
		var Ticket = await _repository.GetTicketAsync(id);

		if (Ticket is null)
		{
			return NotFound();
		}

		return Ticket.ToDto();
	}

	//POST /api/user/
	[HttpPost]
	public async Task<ActionResult<TicketDto>> PostTicket(CreateTicketDto TicketDto)
	{
		TicketModel Ticket = new()
		{
			id = Guid.NewGuid(),
			Subject = TicketDto.Subject,
			Description = TicketDto.Description,
			CreatedDate = DateTimeOffset.UtcNow
		};
		await _repository.CreateTicketAsync(Ticket);

		return CreatedAtAction(nameof(GetTicketAsync), new { id = Ticket.id }, Ticket.ToDto());
	}

	[HttpPatch("{id}")]
	public async Task<ActionResult> UpdateTicket(Guid id, [FromBody]TicketModel ticket)
	{
		var existingTicket = await _repository.GetTicketAsync(id);
		if (existingTicket == null)
		{
			return NotFound();
		}
		
		await _repository.UpdateTicketAsync(id, ticket);
		return Ok(existingTicket);
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult> DeleteTicket(Guid id)
	{
		var existingTicket = await _repository.GetTicketAsync(id);
		if (existingTicket == null)
		{
			return NotFound();
		}

		await _repository.DeleteTicketAsync(id);

		return NoContent();
	}


}