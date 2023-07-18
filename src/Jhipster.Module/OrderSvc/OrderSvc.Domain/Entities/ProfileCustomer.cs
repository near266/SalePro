using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class ProfileCustomer : BaseEntity<Guid>
    {
        public string CustomerName { get; set; }

        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        public string? Position { get; set; }

        public string? Decripstion { get; set; }

        [MaxLength(int.MaxValue)]
        public string? Avatar
        {
            get; set;
        }
    }
}
