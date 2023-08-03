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
    public string? CompanyName { get;set; }
    }
    public class ProfileRes
    {
        public string CustomerName { get; set; }
        public string Username { get; set; }
        public DateTime? DOB { get; set; }

        public Guid? CompanyId { get; set; }

        public string? Position { get; set; }
        public string? Decripstion { get; set; }
        public string? ProductName { get; set; }
        public string? Avatar { get; set; }
        public string? coverImage { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public int? Role { get; set; }
        public int? memberShip { get; set; }
        public string? CompanyName { get; set; }



    }


}
