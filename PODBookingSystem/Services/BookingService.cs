using System;
using System.Collections.Generic;
using System.Linq;
using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Booking CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void CancelBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }

        public Booking GetBookingById(int bookingId)
        {
            return _context.Bookings.Find(bookingId);
        }

        public List<Booking> GetBookingsByCustomerId(int customerId)
        {
            return _context.Bookings.Where(b => b.CustomerId == customerId).ToList();
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public List<Booking> GetAvailablePODs(DateTime date, DateTime startTime, DateTime endTime)
        {
            // Logic để kiểm tra các POD có sẵn trong khoảng thời gian đã cho
            return _context.Bookings
                .Where(b => b.BookingDate.Date == date.Date &&
                            ((b.StartTime >= startTime && b.StartTime < endTime) ||
                             (b.EndTime > startTime && b.EndTime <= endTime)))
                .ToList();
        }
        public IEnumerable<Booking> GetUpcomingBookingsForCustomer(string customerEmail)
        {
            return _context.Bookings
                .Where(b => b.Customer.Email == customerEmail && b.BookingDate > DateTime.Now) // Lấy lịch đặt phòng sắp tới
                .OrderBy(b => b.BookingDate) // Sắp xếp theo ngày
                .ToList();
        }
        public Customer GetCustomerDetails(string customerEmail)
        {
            return _context.Customers
                .FirstOrDefault(c => c.Email == customerEmail); // Lấy thông tin chi tiết của khách hàng
        }
        public IEnumerable<Booking> GetCustomerBookings(int customerId)
        {
            return _context.Bookings.Where(b => b.Customer.Id == customerId).ToList();
        }

        public IEnumerable<Booking> GetCustomerBookings(string customerId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAvailablePODs()
        {
            throw new NotImplementedException();
        }
        public void BookPOD(string username, int podId, int servicePackageId, DateTime bookingDate)
        {
            // Tạo một đối tượng Booking mới
            var booking = new Booking
            {
                Username = username,
                PODId = podId,
                ServicePackageId = servicePackageId,
                BookingDate = bookingDate,
                // Có thể thêm thông tin khác nếu cần
            };

            // Thêm vào cơ sở dữ liệu
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
        public void ApproveBooking(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                // Cập nhật trạng thái đặt chỗ (ví dụ: Approved)
                booking.Status = "Approved"; // Giả sử bạn có thuộc tính Status trong Booking
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
        public IEnumerable<Booking> GetBookingsForToday()
        {
            var today = DateTime.Today;
            return _context.Bookings
                           .Where(b => b.BookingDate.Date == today)
                           .ToList(); // Lấy danh sách các đặt chỗ trong ngày hôm nay
        }
        public bool IsAvailableToday(int podId)
        {
            // Lấy ngày hôm nay
            DateTime today = DateTime.Today;

            // Kiểm tra xem có đặt phòng nào cho POD này trong ngày hôm nay không
            var bookingsToday = _context.Bookings
                .Where(b => b.PODId == podId &&
                            b.StartDate <= today &&
                            b.EndDate >= today)
                .Any();

            // Trả về true nếu không có booking nào (có sẵn), false nếu có booking
            return !bookingsToday;
        }
        public IEnumerable<ServicePackage> GetServicesForToday()
        {
            // Giả sử bạn có một trường để xác định dịch vụ có sẵn hôm nay
            // Thay đổi điều kiện bên dưới theo logic cụ thể của bạn
            return _context.ServicePackages
                           .Where(sp => sp.IsAvailableToday) // Thay đổi điều kiện nếu cần
                           .ToList(); // Lấy danh sách các gói dịch vụ có sẵn hôm nay
        }
        public void MarkAsServiced(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                booking.IsServiced = true; // Giả sử bạn có thuộc tính IsServiced
                _context.SaveChanges();
            }
        }
    }
}
