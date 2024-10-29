using PaymentWebCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebEntity.Entities
{
    public class Payment : BaseEntity
    {
        public decimal Amound { get; set; }

        public Balance Balance { get; set; }

        public int BalanceID { get; set; }

        public User? User { get; set; }

        public int UserID { get; set; }
    }
}
