using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Share.DTO
{
    public class OrderResponse
    {
        public Guid? Id { get; set; }
        public Guid? SalePerson { get; set; }
        public string? SalerName { get; set; }
        public Guid? BoughtPerson { get; set; }
        public int?  Status { get; set; }
        public string? BoughtPersonName { get; set; }
        public string? TransactionId { get;set; }
        public string? TransactionName { get;set; }
        public double? TotalAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string TransactionType { get; set; }
        public AffiliateDTO? affiliate { get; set; }
        public ProductDTOs? product { get; set; }
        public Payment? payment { get; set; }
        [JsonIgnore]
        public IEnumerable<OrderItem>? OrderItem { get; set; }




    }
    public class AffiliateDTO
    {
        public Guid? AffiId { get; set; }
        public Guid? ReferrerId { get; set; }
        public string? ReferrerName { get; set; }
        public double? ReferrerRevenue { get; set; }


        public Guid? ProviderId { get; set; }
        public string? ProviderName { get; set; }  
        public double? ProviderRevenue { get; set; }

        public Guid? SalerId { get; set; }
        public string? SalerName { get;set; }
        public double? SaleRevenue { get; set; }
        public Guid? ParticipantsId { get; set; }
        public string? ParticipantsName { get; set; }
        public double? ParticiantsRevenue { get; set; }

        public string? Ecosystem { get; set; }



    }
    public class ProductDTOs
    {
        public IEnumerable<product>? products { get; set; }


    }
    public class product 
    { 
      public Guid? Id { get; set; }
      public string? ProductName { get; set; }
      public List<string>? Image {  get; set; }

        public double? price { get; set; }
      public Guid? CateId { get;set; }   
      public string? CateName { get; set; } 
     public int? Quantity { get; set; }
      
    }
    public class Payment
    {
        public double? total { get; set; }
        public double? discount { get; set; }
        public double? finalPrice { get; set; }

    }

}
