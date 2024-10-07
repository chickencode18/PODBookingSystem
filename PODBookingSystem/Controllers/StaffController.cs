using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;
using PODBookingSystem.ViewModels;

namespace PODBookingSystem.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IServicePackage _servicePackageService;

        public StaffController(IBookingService bookingService, IServicePackage servicePackageService)
        {
            _bookingService = bookingService;
            _servicePackageService = servicePackageService;
        }

        // Hiển thị bảng điều khiển cho nhân viên.
        public IActionResult Dashboard()
        {
            var bookingsToday = _bookingService.GetBookingsForToday(); // Lấy danh sách đặt chỗ trong ngày
            var servicesToday = _servicePackageService.GetServicesForToday(); // Lấy các dịch vụ cần phục vụ trong ngày
            var staffDashboardData = new StaffDashboardViewModel
            {
                Bookings = bookingsToday,
                Services = servicesToday
            };
            return View(staffDashboardData);
        }

        // Xem các đơn đặt phòng.
        public IActionResult ViewBookings()
        {
            var bookings = _bookingService.GetAllBookings(); // Lấy danh sách các đặt chỗ
            return View(bookings);
        }

        public IActionResult MarkBookingAsServiced(int id)
        {
            _bookingService.MarkAsServiced(id); // Đánh dấu đơn đặt phòng đã hoàn thành dịch vụ
            return RedirectToAction("ViewBookings");
        }

        // Quản lý các dịch vụ đi kèm.
        public IActionResult ManageServices()
        {
            var services = _servicePackageService.GetAllServicePackages(); // Lấy danh sách các dịch vụ đã được đặt
            return View(services);
        }

        public IActionResult MarkServiceAsCompleted(int id)
        {
            _servicePackageService.MarkAsCompleted(id); // Đánh dấu dịch vụ đã hoàn thành
            return RedirectToAction("ManageServices");
        }
        public IActionResult TodayBookings()
        {
            var bookings = _bookingService.GetBookingsForToday(); // Lấy danh sách đặt chỗ trong ngày hôm nay
            return View(bookings);
        }
        public IActionResult TodayServices()
        {
            var services = _servicePackageService.GetServicesForToday(); // Lấy danh sách dịch vụ trong ngày hôm nay
            return View(services);
        }
        public IActionResult SomeAction()
        {
            var services = _servicePackageService.GetServicesForToday();
            // Xử lý và trả về view hoặc kết quả khác
            return View(services);
        }
    }
}
