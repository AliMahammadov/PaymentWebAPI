using PaymentWebCore.Entities;

namespace PaymentWebEntity.Entities
{
    public class Balance : BaseEntity
    {
        public decimal TotalBalace { get; set; }

        public decimal Amound { get; set; }


        public ICollection<Payment> Payments { get; set; }

        public User? User { get; set; }

        public int UserID { get; set; }
    }
}
