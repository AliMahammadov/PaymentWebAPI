using PaymentWebCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentWebEntity.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int? BalanceId { get; set; }
        public Balance Balance { get; set; } 

        public ICollection<Payment>? Payments { get; set; } = new List<Payment>();  // Boş kolleksiya ilə başlatmaq yaxşıdır
    }
}
