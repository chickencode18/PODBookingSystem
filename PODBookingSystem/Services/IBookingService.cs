using System;
using System.Collections.Generic;
using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface IBookingService
    {
        Booking CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void CancelBooking(int bookingId);
        Booking GetBookingById(int bookingId);
        List<Booking> GetBookingsByCustomerId(int customerId);
        List<Booking> GetAllBookings();
        List<Booking> GetAvailablePODs(DateTime date, DateTime startTime, DateTime endTime);
        IEnumerable<Booking> GetUpcomingBookingsForCustomer(string customerEmail);
        Customer GetCustomerDetails(string customerEmail);
        IEnumerable<Booking> GetCustomerBookings(string customerId);
        List<string> GetAvailablePODs();
        void BookPOD(string username, int podId, int servicePackageId, DateTime bookingDate);
        void ApproveBooking(int bookingId);
        IEnumerable<Booking> GetBookingsForToday();
        IEnumerable<ServicePackage> GetServicesForToday();
        void MarkAsServiced(int bookingId);
    }
}
