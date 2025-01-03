using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using MovieReviewApp.Tools;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Net;

namespace MovieReviewApp.Services {
    public class MailService : IMailService {
        private readonly MovieReviewDbContext dbContext;

        public MailService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> SendEmail(int cartId) {
            try {
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if (cart == null) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[400]);
                }

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == cart.UserId);
                if (user == null || string.IsNullOrEmpty(user.Email)) {
                    throw new KeyNotFoundException(ErrorDictionary.ErrorLibrary[400]);
                }

                var fromAddress = new MailAddress("movieapp547@gmail.com", "Movie App");
                var toAddress = new MailAddress(user.Email);
                var message = new MailMessage(fromAddress, toAddress) {
                    Subject = "Order Confirmation",
                    Body = GenerateBody(user),
                    IsBodyHtml = true
                };

                using var smtpClient = new SmtpClient("smtp.gmail.com") {
                    Port = 587,
                    Credentials = new NetworkCredential("movieapp547@gmail.com", "teymbulawswhuazp"),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(message);
                return true;
            } catch (KeyNotFoundException) {
                throw; // Re-throw to be handled by the controller
            } catch (Exception e) {
                throw new Exception("Error generating email:", e);
            }
        }

        public string GenerateBody(User user) {
            var builder = new StringBuilder();
            builder.AppendLine($"<h2>Hello {user.FirstName}, your order has been confirmed. Thanks for shopping!</h2>");
            return builder.ToString();
        }
    }
}
