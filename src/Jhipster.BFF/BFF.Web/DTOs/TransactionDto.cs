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
        public Guid ProductId { get; set; }
        public Guid? SalePerson { get; set; }
        public Guid? BoughtPerson { get; set; }
        public Guid? VoucherId { get; set; }
        public string? TransactionId { get; set; }
        
        public string? TransactionName { get; set; }
        public string TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? PaymentMethod { get; set; }

        [JsonIgnore]
        public Guid? AffiId { get; set; }
        public Guid? ReferrerId { get; set; }
        public Guid? BuyerId { get; set; }
        public Guid? SalerId { get; set; }
        public Guid? ParticipantsId { get; set; }
    }
}
