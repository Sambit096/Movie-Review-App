using MovieReviewApp.Models;
namespace MovieReviewApp.Interfaces
{
    public interface ISMSService
    {
        public Task<bool> SendText(int cartId);
    }
}