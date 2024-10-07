namespace PODBookingSystem.Models
{
    public class Customer : User
    {
        public bool IsVIP { get; set; } = false; // Kiểm tra khách hàng VIP
        public new string Role { get; set; }
    }
}