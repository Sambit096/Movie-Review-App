using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Net;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Services {
    public class MailService : IMailService {
        private readonly MovieReviewDbContext dbContext;

        public MailService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> SendEmail(int cartId) {
            try {
                // Retrieve the cart and check if it exists
                var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.CartId == cartId);
                if (cart == null) {
                    throw new Exception(ErrorDictionary.ErrorLibrary[404] + " Cart not found.");
                }

                // Retrieve the user and check if it exists
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == cart.UserId);
                if (user == null) {
                    throw new Exception(ErrorDictionary.ErrorLibrary[404] + " User not found.");
                }

                // Prepare the email message
                var fromAddress = new MailAddress("youremail@email.com", "Email Testing");
                var toAddress = new MailAddress(user.Email);
                var message = new MailMessage(fromAddress, toAddress) {
                    Subject = "Order Confirmation",
                    Body = GenerateBody(user),
                    IsBodyHtml = true
                };

                // Configure the SMTP client
                using var smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io") {
                    Port = 587,
                    Credentials = new NetworkCredential("mail_trapuser", "mailtrap_pass"),
                    EnableSsl = true
                };

                // Attempt to send the email
                await smtpClient.SendMailAsync(message);
                return true;

            } catch (SmtpException smtpError) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500] + " Failed to send email due to SMTP error: " + smtpError.Message, smtpError);
            } catch (Exception error) {
                throw new Exception(ErrorDictionary.ErrorLibrary[500] + " Error sending email: " + error.Message, error);
            } 
        }

        public string GenerateBody(User user) {
            var builder = new StringBuilder();
            builder.AppendLine($"<h2>Hello {user.FirstName}, your order has been confirmed. Thanks for shopping!</h2>");
            return builder.ToString();
        }
    }
}
