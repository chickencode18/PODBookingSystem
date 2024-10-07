// ServicePackageService.cs
using PODBookingSystem.Models;
using System.Collections.Generic;

namespace PODBookingSystem.Services
{
    public class ServicePackageService : IServicePackage
    {
        private readonly ApplicationDbContext _context;

        public ServicePackageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ServicePackage> GetAvailableServicePackages()
        {
            return _context.ServicePackages.ToList(); // Trả về danh sách các gói dịch vụ
        }

        public ServicePackage GetServicePackageById(int id)
        {
            return _context.ServicePackages.Find(id); // Trả về gói dịch vụ theo ID
        }

        public void AddServicePackage(ServicePackage package)
        {
            _context.ServicePackages.Add(package); // Thêm gói dịch vụ mới
            _context.SaveChanges(); // Lưu thay đổi
        }

        public void UpdateServicePackage(ServicePackage package)
        {
            _context.ServicePackages.Update(package); // Cập nhật gói dịch vụ
            _context.SaveChanges(); // Lưu thay đổi
        }

        public void DeleteServicePackage(int id)
        {
            var package = _context.ServicePackages.Find(id);
            if (package != null)
            {
                _context.ServicePackages.Remove(package); // Xóa gói dịch vụ
                _context.SaveChanges(); // Lưu thay đổi
            }
        }
        public IEnumerable<ServicePackage> GetAllServicePackages()
        {
            return _context.ServicePackages.ToList(); // Trả về danh sách tất cả gói dịch vụ
        }
        public IEnumerable<ServicePackage> GetServicesForToday()
        {
            return _context.ServicePackages
                           .Where(sp => sp.IsAvailableToday) // Hoặc điều kiện khác nếu cần
                           .ToList();
        }
        public void MarkAsCompleted(int servicePackageId)
        {
            var servicePackage = _context.ServicePackages.FirstOrDefault(sp => sp.Id == servicePackageId);
            if (servicePackage != null)
            {
                servicePackage.IsCompleted = true; // Giả sử bạn có thuộc tính IsCompleted
                _context.SaveChanges();
            }
        }
    }
}
