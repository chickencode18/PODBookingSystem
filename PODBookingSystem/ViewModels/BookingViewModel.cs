using PODBookingSystem.Models;
using System;

namespace PODBookingSystem.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int CustomerId { get; set; }
        public string PODName { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<ServicePackage> ServicePackages { get; set; }
    }
}
