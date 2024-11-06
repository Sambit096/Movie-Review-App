using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using MovieReviewApp.Tools;
using System;

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
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(404, "There was a problem retrieving users from the database"), ex);
            }
        }

        public async Task<User> GetUserById(int id) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId == id);
                if (user == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "This user does not exist"));
                }
                return user;
            } catch (KeyNotFoundException knfEx) {
                throw knfEx;
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(500, "Error when getting user"), ex);
            }
        }

        public async Task<User> GetUserByEmail(string email) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "This user does not exist"));
                }
                return user;
            } catch (KeyNotFoundException knfEx) {
                throw knfEx;
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(500, "Error when getting user"), ex);
            }
        }

        public async Task AddUser(User user) {
            try {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(501, "Error when adding new user"), ex);
            }
        }

        public async Task RemoveUser(int id) {
            try {
                var remove = await dbContext.Users.FindAsync(id);
                if (remove != null) {
                    dbContext.Users.Remove(remove);
                    await dbContext.SaveChangesAsync();
                } else {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "This user does not exist"));
                }
            } catch (KeyNotFoundException knfEx) {
                throw knfEx;
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(502, "Error when removing the user"), ex);
            }
        }

        public async Task UpdateUser(User user) {
            try {
                var updateUser = await dbContext.Users.FindAsync(user.UserId);
                if (updateUser == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "This user does not exist"));
                }

                updateUser.Username = user.Username;
                updateUser.Email = user.Email;
                updateUser.FirstName = user.FirstName;
                updateUser.LastName = user.LastName;
                updateUser.Password = user.Password;

                await dbContext.SaveChangesAsync();
            } catch (KeyNotFoundException knfEx) {
                throw knfEx;
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(500, "Error when updating the user"), ex);
            }
        }

        public async Task<User> ValidateUser(string email, string password) {
            try {
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user == null || user.Password != password) {
                    throw new UnauthorizedAccessException(ErrorDictionary.ErrorLibrary.GetValueOrDefault(400, "Invalid email or password"));
                }
                return user;
            } catch (UnauthorizedAccessException uaEx) {
                throw uaEx;
            } catch (Exception ex) {
                throw new Exception(ErrorDictionary.ErrorLibrary.GetValueOrDefault(500, "Error when validating user"), ex);
            }
        }
    }
}
