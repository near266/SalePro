using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Share.DTO
{
    public class PriceDto
    {
     public  double?  TotalPrice { get; set; }
     public double? PriceVoucher { get; set; }
     public  double? RealPice { get; set; }
    }
    public class piricerq { 
    public Guid? ProductId { get; set; }
    public int? quantity { get; set; }
    }

}
