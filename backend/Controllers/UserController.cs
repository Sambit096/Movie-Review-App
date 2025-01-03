﻿using Microsoft.AspNetCore.Mvc;
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
                 return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
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
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
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
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
            }
        }

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser([FromBody] User user) {
            try {
                var updateUser = await this.userService.GetUserById(user.UserId);
                if (updateUser == null) {
                    return StatusCode(404, ErrorDictionary.ErrorLibrary[404]);
                }
                await this.userService.UpdateUser(user);
                return NoContent(); // 204 no content
            } catch (Exception ex) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
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
               return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
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
                return Ok(new { message = "Login successful!", username = user.Username, userId = user.UserId, firstName = user.FirstName, lastName = user.LastName, userType = user.UserType, notiPreference = user.NotiPreference });
            } catch (Exception ex) {
                 return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO) {
            try {
                if (!ModelState.IsValid) return StatusCode(400, ErrorDictionary.ErrorLibrary[400]);
                var user = new User {
                    Email = userDTO.Email,
                    Username = userDTO.Username,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Password = userDTO.Password
                };
                var exists = await this.userService.GetUserByEmail(user.Email);
                if(exists != null) {
                    return StatusCode(500, new {message = "User already exists"});
                }
                await this.userService.AddUser(user);
                return Ok(new { message = "Sign up Successful" });
            } catch (Exception ex) {
                return StatusCode(500, ErrorDictionary.ErrorLibrary[500] + ex);
            }
        }
    }

    public class LoginRequest {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    // Used to pass data transfer object with expected data from front end to the model in backend
    public class UserDTO {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName {get; set; }
        public required string LastName {get; set; }
    }
}
