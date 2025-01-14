using PaymentWebCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace PaymentWebEntity.Entities
{
    public class Balance : BaseEntity
    {
        public decimal TotalBalance { get; set; } = 0;
        public decimal AvailableBalance { get; set; } = 0;


        public ICollection<User> Users { get; set; } = new List<User>();  // Yenə boş kolleksiya ilə başlatmaq yaxşıdır
    }
}
