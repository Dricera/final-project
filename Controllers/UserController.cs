using backend.Dtos;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    // [Authorize] //Implement AddAuthentication and/or ChallengeScheme in Startup
    [ApiController]
	[Route("api/user")]
    [Produces("application/json")]

    public class UserController : ControllerBase

    {
        private readonly IRepository _repository;
        private readonly ILogger<UserController> _logger;

		public UserController(IRepository repository, ILogger<UserController> logger)
		{
			_repository = repository;
			_logger = logger;
		}

/* 		[HttpGet("")]
       public async Task<IActionResult> Get()
        {
            // TODO: Handle DBContext
            // log in console that this was called

            return Content("Here is content");
        } */

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync(string name = null)
        {
            var items = (await _repository.GetItemsAsync())
                        .Select(item => item.AsDto());

            // if (!string.IsNullOrWhiteSpace(name))
            if (name=="")
            {
                items = items.Where(item => item.name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
			_logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()} items");

            return items;
        }

        //GET /api/user/{id}
        [HttpGet("{id}", Name = "User")]
        public async Task <ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item=await _repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }
            
            return item.AsDto();
        }

        //POST /api/user/
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(CreateItemDto itemDto)
        {
            UserModel item = new()
            {
                id = Guid.NewGuid(),
                name = itemDto.name,
                role = itemDto.role,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.id }, item.AsDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

			await _repository.DeleteItemAsync(id);

			return NoContent();
        }

/*
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> PutItem(int id, ItemDto item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            return NoContent();
        }




        private bool ItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
            */

	}
}


