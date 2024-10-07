using PODBookingSystem.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PODBookingSystem.Services
{
    public class NotificationService : INotificationService
    {
        // Ví dụ cho email server configuration
        private readonly string _smtpServer = "smtp.your-email-provider.com";
        private readonly int _smtpPort = 587; // Port cho SMTP
        private readonly string _smtpUser = "your-email@example.com";
        private readonly string _smtpPass = "your-email-password";

        public async Task SendBookingConfirmation(Customer customer, Booking booking)
        {
            string subject = "Xác nhận đặt chỗ";
            string body = $"Xin chào {customer.Name},\n\n" +
                          $"Chúng tôi xin xác nhận rằng bạn đã đặt chỗ thành công.\n" +
                          $"Thông tin đặt chỗ:\n" +
                          $"ID Đặt chỗ: {booking.Id}\n" +
                          $"Ngày: {booking.BookingDate.ToShortDateString()}\n" +
                          $"Thời gian: {booking.StartTime.ToShortTimeString()} - {booking.EndTime.ToShortTimeString()}\n\n" +
                          $"Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!";

            await SendEmailAsync(customer.Email, subject, body);
        }

        public async Task SendBookingCancellation(Customer customer, Booking booking)
        {
            string subject = "Hủy đặt chỗ";
            string body = $"Xin chào {customer.Name},\n\n" +
                          $"Chúng tôi xin thông báo rằng đặt chỗ của bạn đã được hủy thành công.\n" +
                          $"Thông tin đặt chỗ đã hủy:\n" +
                          $"ID Đặt chỗ: {booking.Id}\n" +
                          $"Ngày: {booking.BookingDate.ToShortDateString()}\n" +
                          $"Thời gian: {booking.StartTime.ToShortTimeString()} - {booking.EndTime.ToShortTimeString()}\n\n" +
                          $"Nếu có bất kỳ câu hỏi nào, vui lòng liên hệ với chúng tôi.";

            await SendEmailAsync(customer.Email, subject, body);
        }

        public async Task SendReminder(Customer customer, Booking booking)
        {
            string subject = "Nhắc nhở về đặt chỗ";
            string body = $"Xin chào {customer.Name},\n\n" +
                          $"Đây là lời nhắc nhở về đặt chỗ của bạn sắp tới.\n" +
                          $"Thông tin đặt chỗ:\n" +
                          $"ID Đặt chỗ: {booking.Id}\n" +
                          $"Ngày: {booking.BookingDate.ToShortDateString()}\n" +
                          $"Thời gian: {booking.StartTime.ToShortTimeString()} - {booking.EndTime.ToShortTimeString()}\n\n" +
                          $"Chúng tôi hy vọng được đón tiếp bạn!";

            await SendEmailAsync(customer.Email, subject, body);
        }

        private async Task SendEmailAsync(string email, string subject, string body)
        {
            using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(_smtpUser, _smtpPass);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUser),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        void INotificationService.SendBookingConfirmation(Customer customer, Booking booking)
        {
            throw new NotImplementedException();
        }

        void INotificationService.SendBookingCancellation(Customer customer, Booking booking)
        {
            throw new NotImplementedException();
        }

        void INotificationService.SendReminder(Customer customer, Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
