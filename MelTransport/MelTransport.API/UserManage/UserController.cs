using MelTransport.Application.UserManage;
using MelTransport.Application.UserManage.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MelTransport.API.UserManage
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        {
            var user = await _userService.Create(dto);
            return CreatedAtAction(nameof(GetUserByIdentification), new { id = user.IdentificationNumber }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdentification(int id)
        {
            var user = await _userService.GetByIdentification(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("/Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            if (users == null || !users.Any())
                return NoContent();

            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto dto)
        {
            var updatedUser = await _userService.Update(id, dto);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
