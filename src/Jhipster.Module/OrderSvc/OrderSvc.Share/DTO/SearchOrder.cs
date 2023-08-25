using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Share.DTO
{
    public class SearchOrder
    {
        public Guid Id { get; set; }
        public int? Status { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionName { get;set; }
        public ProductDTOs? ProductDTOs { get; set; }
        ////public Payment? payment { get; set; }
        public double? total { get; set; }
        public double? discount { get; set; }
        public double? finalPrice { get; set; }
    }
    public class TotalResponse {
        public int TotalVoucher {  get; set; }
        public int TotalPtoduct { get; set; }
        public int TotalRequestUpgrade { get; set; }
        public int TotalOrder { get; set; }   
    }

}
