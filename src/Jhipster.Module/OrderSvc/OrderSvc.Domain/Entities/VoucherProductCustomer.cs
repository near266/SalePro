using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class VoucherProductCustomer : BaseEntity<Guid>
    {
        public Guid? CustomerId { get; set; }   
        public Guid? ProductId { get; set;}
        public Guid? VoucherId { get; set; }
        public Product? Product { get; set; }
        public Voucher? Voucher { get; set; }
        public ProfileCustomer? ProfileCustomer { get; set; }
    }
}
