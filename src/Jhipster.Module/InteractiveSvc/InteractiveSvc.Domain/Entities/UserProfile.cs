using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Domain.Entities
{
    public class UserProfile : AuditedEntityBase
    {
        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Fullname { get; set; }
        public DateTime? DOB { get; set; }
        public string? Avatar { get; set; }
        public string? CoverImage { get; set; }
        public string? Position { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public int? Role { get; set; }
    }

}
