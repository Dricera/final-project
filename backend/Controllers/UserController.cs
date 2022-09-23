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
		public async Task<IEnumerable<UserDto>> GetUsersAsync(string name = null)
        {
            var users = (await _repository.GetUsersAsync())
                        .Select(user => user.AsDto());

            // if (!string.IsNullOrWhiteSpace(name))
            if (name=="")
            {
                users = users.Where(user => user.name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
			_logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {users.Count()} users");

            return users;
        }

        //GET /api/user/{id}
        [HttpGet("{id}", Name = "User")]
        public async Task <ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            var user=await _repository.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }
            
            return user.AsDto();
        }

        //POST /api/user/
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(CreateUserDto userDto)
        {
            UserModel user = new()
            {
                id = Guid.NewGuid(),
                name = userDto.name,
                role = userDto.role,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _repository.CreateUserAsync(user);

            return CreatedAtAction(nameof(GetUserAsync), new { id = user.id }, user.AsDto());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var existingUser = await _repository.GetUserAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

			await _repository.DeleteUserAsync(id);

			return NoContent();
        }

/*
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(int id, UserDto user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            return NoContent();
        }




        private bool UserExists(int id)
        {
            return _context.TodoUsers.Any(e => e.Id == id);
        }
            */

	}
}


