using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Models;
using PODBookingSystem.Services;

namespace PODBookingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private readonly IBookingService _bookingService;

        public AdminController(IUserService userService, IReportService reportService, IBookingService bookingService)
        {
            _userService = userService;
            _reportService = reportService;
            _bookingService = bookingService;
        }

        public IActionResult Dashboard()
        {
            var reportData = _reportService.GetDashboardData(); // Lấy dữ liệu thống kê cho Dashboard
            return View(reportData);
        }

        public IActionResult ManageUsers()
        {
            var users = _userService.GetAllUsers(); // Lấy danh sách tất cả người dùng
            return View(users);
        }

        public IActionResult EditUser(int id)
        {
            var user = _userService.GetUserById(id); // Lấy thông tin người dùng cụ thể
            return View(user);
        }

        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id); // Xóa người dùng khỏi hệ thống
            return RedirectToAction("ManageUsers");
        }

        public IActionResult ViewReports()
        {
            var reports = _reportService.GetUsageReports(); // Lấy các báo cáo chi tiết
            return View(reports);
        }
    }
}
