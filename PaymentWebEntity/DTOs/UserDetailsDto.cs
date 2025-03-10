﻿using PaymentWebCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentWebEntity.DTOs
{
    public class UserDetailsDto:BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal TotalBalance { get; set; }
        public List<PaymentDto>? Payments { get; set; }
    }

    public class PaymentDto:BaseEntity
    {
        public decimal? Amount { get; set; }
        public int UserId { get; set; }
    }
}
