using PODBookingSystem.Models;
using PODBookingSystem.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public DashboardData GetDashboardData()
    {
        throw new NotImplementedException();
    }

    public DashboardData GetManagerDashboardData()
    {
        var totalBookings = _context.Bookings.Count();
        var totalRevenue = _context.Bookings.Sum(b => b.Price);
        var totalCustomers = _context.Customers.Count();
        var activeStaff = _context.Staffs.Count(s => s.IsActive);

        return new DashboardData
        {
            TotalBookings = totalBookings,
            TotalRevenue = totalRevenue,
            TotalCustomers = totalCustomers,
            ActiveStaff = activeStaff
        };
    }

    public IEnumerable<Report> GetUsageReports()
    {
        throw new NotImplementedException();
    }
}
