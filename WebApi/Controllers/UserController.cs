using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using web.Core.DTOs;
using web.Core.models;
using web.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            if (users == null || users.Count == 0)
                return NotFound("No users found.");
            return Ok(users);
        }

        // GET api/User/5
        [HttpGet("userId/{id}")] //מקבלת USERID ומחזירה לפיו
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");
            return Ok(user);
        }
        // GET api/User/5
        [HttpGet("userTz/{identity}")] //מקבלת ID ומחזירה לפיו
        public async Task<ActionResult<User>> GetUserByIdentityAsync(int identity)
        {
            var user = await _userService.GetUserByIdentityAsync(identity);
            if (user == null)
                return NotFound($"User with ID {identity} not found.");
            return Ok(user);
        }

        // GET api/User/5
        [HttpGet("userEmail/{id}")]
        public async Task<ActionResult<User>> GetUserByEmailAsync(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound($"User with EMAIL {email} not found.");
            return Ok(user);
        }

        // POST api/User
        [HttpPost("registerAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddAdminAsync([FromBody] UserPostDTO user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            var success = await _userService.AddAdminAsync(user);
            if (!success)
                return BadRequest("Failed to add admin.");

            var user1 = await _userService.GetUserByEmailAsync(user.Email);
            var token = _userService.GenerateJwtToken(user1.Id,user.UserId, user.Name, user.Email, ERole.User.ToString());
            return Ok(new { Token = token, Message = "Admin added successfully." });
        }


        // POST api/User
        [HttpPost("registerUser")]
        public async Task<ActionResult> AddUseAsync([FromBody] UserPostDTO user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            var success = await _userService.AddUserAsync(user);
            if (!success)
                return BadRequest("Failed to add user.");

            var user1 = await _userService.GetUserByEmailAsync(user.Email);
            var token = _userService.GenerateJwtToken(user1.Id,user.UserId, user.Name, user.Email, ERole.User.ToString());
            return Ok(new { Token = token, Message = "User added successfully." });
        }


        //כניסה למשתמש מחובר
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO loginUser)
        {
            Console.WriteLine(loginUser);
            var user = await _userService.GetUserByEmailAsync(loginUser.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash))
                return Unauthorized();

            var token = _userService.GenerateJwtToken(user.Id,user.UserId,user.Name,user.Email, user.Role.ToString());
            return Ok(new { Token = token });
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] UserUpdateDTO user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            var success = await _userService.UpdateUserAsync(id, user);
            if (!success)
                return NotFound($"User with ID {id} not found.");

            var userCurrent = await _userService.GetUserByIdAsync(id);

            var token = _userService.GenerateJwtToken(userCurrent.Id,userCurrent.Id, userCurrent.Name, userCurrent.Email, userCurrent.Role.ToString());

            return Ok(new { Token = token, Message = "User updated successfully." });
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
                return NotFound($"User with ID {id} not found.");
            return Ok(new { Message = "User deleted successfully." });
        }
    }
}
