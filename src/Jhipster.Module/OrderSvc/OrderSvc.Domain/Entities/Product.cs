
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        public string? ProductName { get; set; }
        public double? Price { get; set; }
        public double? PriceNum { get; set; }

        public string? ProviderId { get;set; }
        public string? warranty { get; set; }
      
        public List<string>? Image {  get; set; }
        public string? Decripstion { get; set; }
        public virtual CategoryProduct? CategoryProduct { get; set; }
        
     
    }
}
