using PaymentWebCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace PaymentWebEntity.Entities
{
    public class User: BaseEntity
    {
        [MaxLength(10), MinLength(3), Required]
        public string UserName { get; set; }

        [MaxLength(15), MinLength(3), Required]
        public string SurName { get; set; }

        [MaxLength(7), MinLength(7), Required]
        public string PasswordID { get; set; }

        [Required]
        public DateTime? Birthday { get; set; } = new DateTime(1995, 5, 23);

    

        public ICollection<Payment> Payments { get; set; }

        public Balance? Balance { get; set; }

        public int BalanceID { get; set; }
    }
}
