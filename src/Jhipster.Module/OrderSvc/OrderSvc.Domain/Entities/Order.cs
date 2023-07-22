using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? CustomserId { get; set; }  
        public ProfileCustomer? Customer { get; set; }
        public string? TransactionId { get; set; }
        public Transactions? Transactions { get; set; }

    }
}
