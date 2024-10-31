using Microsoft.AspNetCore.Mvc;
using MovieReviewApp.Models;
using MovieReviewApp.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        [HttpPost(nameof(AddUser))]
        public async Task<IActionResult> AddUser(User user) {
            try {
                await this.userService.AddUser(user);
                return Ok(user);
            } catch (Exception ex) {
                return StatusCode(500, $"Error when adding User: {ex}");
            }
        }

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(int id, User user) {
            try {
                var updateUser = await this.userService.GetUserById(id);
                if (updateUser == null) {
                    return NotFound();
                }
                await this.userService.UpdateUser(id, user);
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
                await this.userService.RemoveUser(id);
                return NoContent(); // 204 no content
            } catch (Exception ex) {
                return StatusCode(500, $"Error when removing user: {ex}");
            }
        }
    }
}
