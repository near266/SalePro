using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BFF.Web.DTOs
{
    public class TransactionDto
    {

        [JsonIgnore]
        public Guid Id { get; set; }
        public List<ProductRq>? Products { get; set; }
        public Guid? SalePerson { get; set; }
        public Guid? BoughtPerson { get; set; }
        public Guid? VoucherId { get; set; }

        [JsonIgnore]

        public string? TransactionId { get; set; }
        [JsonIgnore]

        public Guid? AffiliateId { get; set; }


        public double? TotalAmount { get; set; }

        public string? TransactionName { get; set; }
        public string TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? PaymentMethod { get; set; }

       
        public Guid? ReferrerId { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SalerId { get; set; }
        public Guid? ParticipantsId { get; set; }
    }
    public class ProductRq { 
     public int quantity { get; set; }
     public Guid? ProductId { get; set; }
    }

}
