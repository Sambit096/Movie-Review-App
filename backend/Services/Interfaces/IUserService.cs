using MovieReviewApp.Models;

namespace MovieReviewApp.Interfaces {
    public interface IUserService {
        Task<IList<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task RemoveUser(int id);
        Task UpdateUser(int id, User user);
    }
}
