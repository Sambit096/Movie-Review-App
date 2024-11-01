using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces
{
    public interface IMailService
    {
        public Task<bool> SendEmail(int cartId);
    }
}