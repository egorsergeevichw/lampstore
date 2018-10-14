using System;
using System.Net;
using System.Net.Mail;
using LampStore.Domain.Models;

namespace LampStore.Domain.Utils
{
    public static class EmailUtils
    {
        public static void SendRegistrationEmail(string userName, Guid userId, string userEmail)
        {
            const string subject = "Registration success!";

            var body = $"Dear <strong>{userName}</strong>,<br/>" +
                       "Thank you for registering on the <strong>classiclamp.com.</strong><br/><br/>" +
                       "Please confirm your email address by click on " +
                       $"<a href=\"http://localhost:63228/auth/login?userId={userId}\">this link.</a><br/><br/>" +
                       "If you have any questions please contact with us: <strong>+7(999)999-99-99.</strong>";

            SendEmail(userEmail, subject, body);
        }

        public static void SendFeedbackEmail(string name, string email, string message)
        {
            const string subject = "Feedback email!";

            var body = $"<strong>From:</strong> {name}.<br/>" +
                       $"<strong>Email</strong>: {email}.<br/>" +
                       "<strong>Message:</strong><br/>" +
                       $"{message}";

            SendEmail("egor.vdovenko2018@yandex.ru", subject, body);
        }
        public static void SendAdminOrderEmail()
        {
            const string subject = "New order!";

            var body = "Check new order on the site!";

            SendEmail("egor.vdovenko2018@yandex.ru", subject, body);
        }

        public static void SendUserOrderEmail(string userName, string userEmail, OrderModel order)
        {
            const string subject = "Order success!";

            var items = "<ul>";

            foreach (var item in order.OrderItems)
            {
                items = string.Concat(items, $"<li>{item.Product.Name} ({item.Count} x {item.Product.Price})</li>");
            }

            items = string.Concat(items, "</ul>");

            var body = $"Dear <strong>{userName}</strong>, thank you for our oder!<br/><br/>" +
                       "Order items:<br/>" +
                       items +
                       $"Total price: <strong>&euro;{order.TotalPrice}</strong><br/><br/>" +
                       "Our specialist will contact you in the near future, that would clarify the details of the order.<br/>" +
                       "If you have any questions please contact with us: <strong>+7(999)999-99-99.</strong>";

            SendEmail(userEmail, subject, body);
        }

        public static void SendEmail(string to, string subject, string body)
        {
            const string from = "yodaskillme@gmail.com";
            const string password = "gbkfgbkf123";
            const string host = "smtp.gmail.com";

            var msg = new MailMessage(from, to, subject, body);

            msg.IsBodyHtml = true;

            var client = new SmtpClient(host, 587)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true,
            };

            client.Send(msg);
        }
    }
}
