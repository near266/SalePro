using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Application.DTO
{
    public class MeetingUpdateDTO
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Avatar { get; set; }
        public string? Fullname { get; set; }
        public string? Title { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
    }
}
