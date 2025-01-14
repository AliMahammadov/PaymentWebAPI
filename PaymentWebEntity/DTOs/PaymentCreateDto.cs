using PaymentWebEntity.Entities;

namespace PaymentWebEntity.DTOs
{
    public class PaymentCreateDto
    {
        public decimal Amount { get; set; }

        public int UserId { get; set; }
        public int BalanceId { get; set; }
    }
}
