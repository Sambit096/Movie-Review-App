using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using MovieReviewApp.Tools;
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
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }

        }
        public async Task<User> GetUserById(int id) {
             try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId == id);
                return user;
            }
             catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }
        public async Task<User?> GetUserByEmail(string email) {
            try
            {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
                return user;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        public async Task AddUser(User user) {
            try {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
               throw new Exception(ErrorDictionary.ErrorLibrary[500]);
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
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
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
                    updateUser.AgeGroup = user.AgeGroup;
                    updateUser.Gender = user.Gender;
                    updateUser.NotiPreference = user.NotiPreference;
                    updateUser.UserType = user.UserType;
                    await dbContext.SaveChangesAsync();
                }
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        public async Task<User> ValidateUser(string email, string password) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null) return null;
                if (user.Password == password) return user; // Hash for future security reasons
                return null;
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }
    }
}
