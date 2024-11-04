using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces {
    public interface IUserService {
        Task<IList<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
        Task RemoveUser(int id);
        Task UpdateUser(User user);
        Task<User> ValidateUser(string email, string password);
    }
}
