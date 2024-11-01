using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
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
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == cart.UserId);

                var fromAddress = new MailAddress("youremail@email.com", "Email Testing");
                var toAddress = new MailAddress(user.email);
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Order Confirmation",
                    Body = GenerateBody(user),
                    IsBodyHtml = true
                };

                using var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mail_trapuser", "mailtrap_pass"),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(message);
                return true;
            } catch (Exception error) {
                throw new Exception("Error sending email:", error);
                return false;
            } 
        }

        public string GenerateBody(User user)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"<h2>Hello {user.firstName}, your order has been confirmed. Thanks for shopping!</h2>");
            return builder.ToString();
        }
    }
}