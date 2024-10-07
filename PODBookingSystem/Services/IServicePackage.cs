using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface IServicePackage
    {
        IEnumerable<ServicePackage> GetAvailableServicePackages(); // Lấy danh sách các gói dịch vụ có sẵn
        ServicePackage GetServicePackageById(int id); // Lấy thông tin một gói dịch vụ theo ID
        void AddServicePackage(ServicePackage package); // Thêm gói dịch vụ mới
        void UpdateServicePackage(ServicePackage package); // Cập nhật gói dịch vụ
        void DeleteServicePackage(int id); // Xóa gói dịch vụ
        IEnumerable<ServicePackage> GetAllServicePackages();
        IEnumerable<ServicePackage> GetServicesForToday();
        void MarkAsCompleted(int servicePackageId);
    }
}