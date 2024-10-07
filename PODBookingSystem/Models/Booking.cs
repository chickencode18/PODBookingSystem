using System;

namespace PODBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; } // ID của đặt chỗ
        public string Username { get; set; } // Tên người dùng (hoặc email)
        public DateTime BookingDate { get; set; } // Ngày đặt
        public int PODId { get; set; } // ID của POD
        public int ServicePackageId { get; set; } // ID của gói dịch vụ
        public double Price { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }


        public virtual Customer Customer { get; set; }
        public bool IsServiced { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
