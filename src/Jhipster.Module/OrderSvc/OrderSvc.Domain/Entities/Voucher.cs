﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Voucher 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? VoucherProCusId { get;set; }
        public VoucherProductCustomer? VoucherProductCustomer { get; set; }
        public string CodeVoucher { get;set; }
        public List<string>? Image { get; set; }
        public int? isExpired { get;set; }
        public string? Description { get;set; }
        public decimal? Discount { get;set; }
        public int ? Status { get;set; }
        public DateTime? CreatedDate { get;set; }
        public DateTime? EndDate { get;set;}
    }
}
