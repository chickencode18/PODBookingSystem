using PODBookingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PODBookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context; // Context cơ sở dữ liệu

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả người dùng từ cơ sở dữ liệu
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Lấy người dùng theo ID từ cơ sở dữ liệu
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        // Xóa người dùng theo ID từ cơ sở dữ liệu
        public void DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Xác thực người dùng bằng email và mật khẩu từ cơ sở dữ liệu
        public User ValidateUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password; // Cẩn thận với việc lưu mật khẩu
                // Cập nhật thêm các thuộc tính khác nếu cần
                _context.SaveChanges();
            }
        }
    }
}
