using PaymentWebCore.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaymentWebEntity.Entities
{
    public class User : BaseEntity
    {

        [MaxLength(20)]
        public string? FullName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string RepeatPassword { get; set; }

        [Required, Phone, MaxLength(10), MinLength(10)]
        public string PhoneNumber { get; set; }

        public int? BalanceId { get; set; }
        public Balance Balance { get; set; } 

        public ICollection<Payment>? Payments { get; set; } = new List<Payment>(); 
    }
}
