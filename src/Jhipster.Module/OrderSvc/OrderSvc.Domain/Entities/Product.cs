
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
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public List<string>? Image {  get; set; }
        public string? Decripstion { get; set; }
        public virtual CategoryProduct? CategoryProduct { get; set; }
    }
}
