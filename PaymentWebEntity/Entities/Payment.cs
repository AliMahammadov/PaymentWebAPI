using PaymentWebCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace PaymentWebEntity.Entities
{
    public class Payment : BaseEntity
    {
        public decimal? Amount { get; set; } = 0;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
