using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;
using PODBookingSystem.ViewModels;

namespace PODBookingSystem.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IServicePackage _servicePackageService;

        public CustomerController(IBookingService bookingService, IServicePackage servicePackageService)
        {
            _bookingService = bookingService;
            _servicePackageService = servicePackageService;
        }

        // Hiển thị bảng điều khiển cho khách hàng.
        public IActionResult Dashboard()
        {
            var upcomingBookings = _bookingService.GetUpcomingBookingsForCustomer(User.Identity.Name); // Lấy lịch đặt phòng sắp tới cho khách hàng hiện tại
            var customerDetails = _bookingService.GetCustomerDetails(User.Identity.Name); // Lấy chi tiết khách hàng
            var dashboardViewModel = new CustomerDashboardViewModel
            {
                UpcomingBookings = (List<BookingViewModel>)upcomingBookings,
                CustomerDetails = customerDetails
            };
            return View(dashboardViewModel);
        }

        // Hiển thị lịch sử đặt phòng của khách hàng.
        public IActionResult Bookings()
        {
            var bookings = _bookingService.GetCustomerBookings(User.Identity.Name); // Lấy lịch sử đặt phòng cho khách hàng hiện tại
            return View(bookings);
        }

        // Xem các gói dịch vụ có sẵn.
        public IActionResult ViewServicePackages()
        {
            var servicePackages = _servicePackageService.GetAvailableServicePackages(); // Lấy danh sách các gói dịch vụ
            return View(servicePackages);
        }

        // Khách hàng thực hiện đặt phòng.
        public IActionResult MakeBooking()
        {
            var availablePODs = _bookingService.GetAvailablePODs(); // Lấy danh sách các phòng trống
            var servicePackages = _servicePackageService.GetAvailableServicePackages(); // Lấy danh sách các gói dịch vụ
            var bookingViewModel = new MakeBookingViewModel
            {
                AvailablePODs = availablePODs,
                ServicePackages = servicePackages // Đảm bảo rằng bạn đang gán danh sách gói dịch vụ đúng cách
            };
            return View(bookingViewModel);
        }

        [HttpPost]
        public IActionResult MakeBooking(MakeBookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện đặt phòng và lưu vào cơ sở dữ liệu.
                _bookingService.BookPOD(User.Identity.Name, model.SelectedPODId, model.SelectedServicePackageId, model.BookingDate);
                return RedirectToAction("Dashboard");
            }
            return View(model);
        }
    }
}
