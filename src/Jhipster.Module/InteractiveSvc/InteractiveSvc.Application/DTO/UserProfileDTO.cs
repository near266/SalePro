using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Application.DTO
{
    public class UserUpdateDTO
    {
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
