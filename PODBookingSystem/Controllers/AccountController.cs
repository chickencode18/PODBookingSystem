using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PODBookingSystem.Services;
using System.Security.Claims;
using PODBookingSystem.ViewModels;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = _userService.ValidateUser(email, password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Điều hướng dựa trên vai trò của người dùng
            if (user.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (user.Role == "Manager")
            {
                return RedirectToAction("Dashboard", "Manager");
            }
            else if (user.Role == "Staff")
            {
                return RedirectToAction("Dashboard", "Staff");
            }
            else if (user.Role == "Customer")
            {
                return RedirectToAction("Dashboard", "Customer");
            }
        }

        ViewBag.ErrorMessage = "Email hoặc mật khẩu không chính xác!";
        return View();
    }
}
