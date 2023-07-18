using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class CategoryProduct :BaseEntity<Guid>
    {
    public Guid? ProductId { get; set; }
    public Guid? CategoryId { get; set; }
    public Product? Product { get; set; }
    public Category? Category { get; set; }
    }
}
