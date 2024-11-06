using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Tools;
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
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return Ok(users);
            } catch (Exception ex) {
                 return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        [HttpGet(nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id) {
            try {
                var user = await this.userService.GetUserById(id);
                if(user == null) {
                     return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return Ok(user);
            } catch (Exception ex) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        [HttpGet(nameof(GetUserByEmail))]
        public async Task<IActionResult> GetUserByEmail(string email) {
            try {
                var user = await this.userService.GetUserByEmail(email);
                if(user == null) {
                      return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                return Ok(user);
            } catch (Exception ex) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(User user) {
            try {
                var updateUser = await this.userService.GetUserById(user.UserId);
                if (updateUser == null) {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                await this.userService.UpdateUser(user);
                return NoContent(); // 204 no content
            } catch (Exception ex) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        [HttpDelete(nameof(RemoveUser))]
        public async Task<IActionResult> RemoveUser(int id) {
            try {
                var removeUser = await this.userService.GetUserById(id);
                if (removeUser == null) {
                     return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                await this.userService.RemoveUser(removeUser.UserId);
                return NoContent(); // 204 no content
            } catch (Exception ex) {
               return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request) {
            try {
                if (!ModelState.IsValid) return StatusCode(400, ErrorDictionary.ErrorLibrary[400]);

                var user = await this.userService.ValidateUser(request.Email, request.Password);
                if (user == null) {
                    return StatusCode(401, ErrorDictionary.ErrorLibrary[401]);
                }
                return Ok(new { message = "Login successful!" });
            } catch (Exception ex) {
                 return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO) {
            try {
                if (!ModelState.IsValid) return StatusCode(400, ErrorDictionary.ErrorLibrary[400]);
                var user = new User {
                    Email = userDTO.Email,
                    Username = userDTO.Username,
                    FirstName = "",
                    LastName = "",
                    Password = userDTO.Password
                };
                await this.userService.AddUser(user);
                return StatusCode(201);
            } catch (Exception ex) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500]);
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
