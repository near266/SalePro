using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Share.DTO
{
    public class ProductDTO
    {
        public Guid Id {  get; set; }
        public string ProductName { get; set; }
        public double? Price { get; set; }
        public double? PriceNum { get; set; }
        public string? Provider { get; set; }
        public string? ProviderId { get; set; }
        public string? warranty { get; set; }
        public Guid? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public List<string>? Image { get; set; }
        public string? Decripstion { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
