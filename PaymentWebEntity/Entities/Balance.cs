using PaymentWebCore.Entities;

namespace PaymentWebEntity.Entities
{
    public class Balance : BaseEntity
    {
        public decimal TotalBalance { get; set; } 
        public decimal AvailableBalance { get; set; } 


        public ICollection<User> Users { get; set; } = new List<User>(); 
    }
}
