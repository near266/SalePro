﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? SalePerson { get; set; }
        public Guid? BoughtPerson { get; set; }
        public Guid? VoucherId { get; set; }
        public string? TransactionId { get; set; }
        public Transactions? Transactions { get; set; }
        public Guid? AffiliateId {  get; set; }
        public Affiliates? Affiliates { get; set;}
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(100)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public IEnumerable<OrderItem> OrderItem{ get; set; }

    }
}
