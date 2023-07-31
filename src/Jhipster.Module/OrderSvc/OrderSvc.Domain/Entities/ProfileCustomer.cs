using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderSvc.Domain.Entities
{
    public class ProfileCustomer : BaseEntity<Guid>
    {
        public string CustomerName { get; set; }
        public string Username { get; set; }
        public DateTime? DOB { get; set; }
        [JsonIgnore]
        public Guid? CompanyId { get; set; }
        public string? Position { get; set; }
        public string? Decripstion { get; set; }

        public string? Avatar {get; set; }
        public string? coverImage { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public int? Role { get; set; }
        public int? memberShip { get; set; }

    }
}
