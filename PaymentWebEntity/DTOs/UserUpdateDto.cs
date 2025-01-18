using PaymentWebCore.Entities;

namespace PaymentWebEntity.DTOs
{
    public class UserUpdateDto:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
