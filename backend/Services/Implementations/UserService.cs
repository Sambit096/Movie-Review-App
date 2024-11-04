using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Runtime.CompilerServices;


namespace MovieReviewApp.Implementations {
    public class UserService : IUserService {
        private readonly MovieReviewDbContext dbContext;

        public UserService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IList<User>> GetUsers() {
            try {
                return await dbContext.Users.ToListAsync();
            } catch (Exception ex) {
                throw new Exception("There was a problem retrieving users from the database: ", ex);
            }

        }
        public async Task<User> GetUserById(int id) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId == id);
                if (user == null) {
                    throw new Exception("This user does not exist");
                }
                return user;
            } catch (Exception ex) {
                throw new Exception("Error when getting user: ", ex);
            }
        }
        public async Task<User> GetUserByEmail(string email) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.email == email);
                if(user == null) {
                    throw new Exception("This user does not exist");
                }
                return user;
            } catch (Exception ex) {
                throw new Exception("Error when getting user: ", ex);
            }
        }

        public async Task AddUser(User user) {
            try {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception("Error when adding new user: ", ex);
            }
        }
        public async Task RemoveUser(int id) {
            try {
                var remove = await dbContext.Users.FindAsync(id);
                if(remove != null) {
                    dbContext.Remove(remove);
                    await dbContext.SaveChangesAsync();
                }
            } catch (Exception ex) {
                throw new Exception("Error when removing the user: ", ex);
            }
        }

        public async Task UpdateUser(User user) {
            try {
                var updateUser = await dbContext.Users.FindAsync(user.UserId);
                if(updateUser != null) {
                    updateUser.Username = user.Username;
                    updateUser.Email = user.Email;
                    updateUser.FirstName = user.FirstName;
                    updateUser.LastName = user.LastName;
                    updateUser.Password = user.Password;
                    await dbContext.SaveChangesAsync();
                }
            } catch (Exception ex) {
                throw new Exception("Error when updating the user: ", ex);
            }
        }

        public async Task<User> ValidateUser(string email, string password) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.email == email);
                if (user == null) return null;
                if (user.password == password) return user; // Hash for future security reasons
                return null;
            } catch (Exception ex) {
                throw new Exception("Error when validating user: ", ex);
            }
        }
    }
}
