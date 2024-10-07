using System.ComponentModel.DataAnnotations;

namespace PODBookingSystem.Models
{
    public class ServicePackage
    {
        public int Id { get; set; } // ID của gói dịch vụ

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Tên gói dịch vụ

        [Required]
        [StringLength(50)]
        public string Code { get; set; } // Mã gói dịch vụ

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        public double Price { get; set; } // Giá gói dịch vụ

        public string Description { get; set; } // Mô tả (tùy chọn)
        public bool IsAvailableToday { get; set; }
        public bool IsCompleted { get; set; }
    }
}
