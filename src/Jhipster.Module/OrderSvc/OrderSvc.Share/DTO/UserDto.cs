using OrderSvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSvc.Share.DTO
{
    public class userResponse { 
    public ProfileCustomer? profileCustomer { get;set; }
    public IEnumerable <string>?products { get;set; }
        public string? CompanyName { get;set; }
    }

}
