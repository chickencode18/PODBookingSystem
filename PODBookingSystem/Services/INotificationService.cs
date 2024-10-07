using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface INotificationService
    {
        void SendBookingConfirmation(Customer customer, Booking booking);
        void SendBookingCancellation(Customer customer, Booking booking);
        void SendReminder(Customer customer, Booking booking);
    }
}
