using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Identity.Data;

namespace MovieReviewApp.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private readonly IUserService userService;

        public UserController(IUserService userService) {
            this.userService = userService;
        }

        [HttpGet(nameof(GetUsers))]
        public async Task<IActionResult> GetUsers() {
            try {
                var users = await this.userService.GetUsers();
                if(users == null || !users.Any()) {
                    return NotFound();
                }
                return Ok(users);
            } catch (Exception ex) {
                return StatusCode(500, $"Error when recieving User data: {ex}");
            }
        }

        [HttpGet(nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id) {
            try {
                var user = await this.userService.GetUserById(id);
                if(user == null) {
                    return NotFound();
                }
                return Ok(user);
            } catch (Exception ex) {
                return StatusCode(500, $"Error when recieving user Data: {ex}");
            }
        }

        [HttpGet(nameof(GetUserByEmail))]
        public async Task<IActionResult> GetUserByEmail(string email) {
            try {
                var user = await this.userService.GetUserByEmail(email);
                if(user == null) {
                    return NotFound();
                }
                return Ok(user);
            } catch (Exception ex) {
                return StatusCode(500, $"Error when getting the user: {ex}");
            }
        }

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(User user) {
            try {
                var updateUser = await this.userService.GetUserById(user.UserId);
                if (updateUser == null) {
                    return NotFound();
                }
                await this.userService.UpdateUser(user);
                return NoContent(); // 204 no content
            } catch (Exception ex) {
                return StatusCode(500, $"Error when updating user data: {ex}");
            }
        }

        [HttpDelete(nameof(RemoveUser))]
        public async Task<IActionResult> RemoveUser(int id) {
            try {
                var removeUser = await this.userService.GetUserById(id);
                if (removeUser == null) {
                    return NotFound();
                }
                await this.userService.RemoveUser(removeUser.UserId);
                return NoContent(); // 204 no content
            } catch (Exception ex) {
                return StatusCode(500, $"Error when removing user: {ex}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            try {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = await this.userService.ValidateUser(request.Email, request.Password);
                if (user == null) {
                    return Unauthorized(new { message = "Invalid email or password." });
                }
                return Ok(new { message = "Login successful!" });
            } catch (Exception ex) {
                return StatusCode(500, $"Error when logging in user: {ex}");
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO) {
            try {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var user = new User {
                    Email = userDTO.Email,
                    Username = userDTO.Username,
                    FirstName = "",
                    LastName = "",
                    Password = userDTO.Password
                };
                await this.userService.AddUser(user);
                return Ok(user);
            } catch (Exception ex) {
                return StatusCode(500, $"Error when adding User: {ex}");
            }
        }
    }

    public class LoginRequest {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Used to pass data transfer object with expected data from front end to the model in backend
    public class UserDTO {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
