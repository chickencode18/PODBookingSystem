using PODBookingSystem.Models;

namespace PODBookingSystem.Services
{
    public interface IReportService
    {
        DashboardData GetDashboardData();    
        IEnumerable<Report> GetUsageReports();
        DashboardData GetManagerDashboardData();
    }
}
