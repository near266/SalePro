using OrderSvc.Domain.Entities;
using OrderSvc.Share.DTO;
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

        public Guid? Id { get; set; }
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

        public Guid? ProviderId { get; set; }

        public Guid? ReferrerId { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SalerId { get; set; }
        public Guid? ParticipantsId { get; set; }
    }
    public class ProductRq { 
     public int quantity { get; set; }
     public Guid? ProductId { get; set; }
    }

    public class ResponseSearchOrderDto {
        public Guid? Id { get; set; }
        public Guid? SalePerson { get; set; }
        public string? SalerName { get; set; }
        public Guid? BoughtPerson { get; set; }
        public int? Status { get; set; }
        public string? BoughtPersonName { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionName { get; set; }
        public double? TotalAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string TransactionType { get; set; }
        public AffiliateDTO? affiliate { get; set; }
        public ProductDTOs? product { get; set; }
        public Payment? payment { get; set; }
    }

}
