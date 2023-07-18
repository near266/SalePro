using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Company : BaseEntity<Guid>
    {
        public string CompanyName { get; set; }
        public virtual ICollection<ProfileCustomer>? Customer { get; set; }
        public virtual ICollection<Product>? Products { get;}
        [MaxLength(int.MaxValue)]
        public string? Avatar
        {
            get; set;
        }
    }
}
