using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;

namespace PODBookingSystem.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IServicePackage _servicePackageService;
        private readonly IReportService _reportService;

        public ManagerController(IBookingService bookingService, IServicePackage servicePackageService, IReportService reportService)
        {
            _bookingService = bookingService;
            _servicePackageService = servicePackageService;
            _reportService = reportService;
        }

        // Hiển thị bảng điều khiển cho quản lý.
        public IActionResult Dashboard()
        {
            var dashboardData = _reportService.GetManagerDashboardData();
            return View(dashboardData);
        }

        // Quản lý các đặt phòng.
        public IActionResult ManageBookings()
        {
            var bookings = _bookingService.GetAllBookings(); // Lấy tất cả các đặt chỗ
            return View(bookings);
        }

        public IActionResult ApproveBooking(int id)
        {
            _bookingService.ApproveBooking(id); // Phê duyệt đặt chỗ
            return RedirectToAction("ManageBookings");
        }

        public IActionResult CancelBooking(int id)
        {
            _bookingService.CancelBooking(id); // Hủy đặt chỗ
            return RedirectToAction("ManageBookings");
        }

        // Quản lý các gói dịch vụ.
        public IActionResult ViewServicePackages()
        {
            var servicePackages = _servicePackageService.GetAllServicePackages(); // Lấy tất cả các gói dịch vụ
            return View(servicePackages);
        }

        public IActionResult EditServicePackage(int id)
        {
            var servicePackage = _servicePackageService.GetServicePackageById(id); // Lấy thông tin gói dịch vụ cụ thể
            return View(servicePackage);
        }

        public IActionResult DeleteServicePackage(int id)
        {
            _servicePackageService.DeleteServicePackage(id); // Xóa gói dịch vụ
            return RedirectToAction("ViewServicePackages");
        }
        public IActionResult ServicePackages()
        {
            var servicePackages = _servicePackageService.GetAllServicePackages(); // Lấy danh sách tất cả gói dịch vụ
            return View(servicePackages);
        }
    }
}
