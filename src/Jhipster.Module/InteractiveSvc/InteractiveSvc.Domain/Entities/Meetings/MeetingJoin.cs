using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Domain.Entities.Meetings
{
    public class MeetingJoin : BaseEntity<Guid>
    {
        [ForeignKey("Meetings")]
        public Guid? MeetingId { get; set; }
        public Meeting? Meetings { get; set; }
        [ForeignKey("UserProfiles")]
        public string? UserId { get; set; }
        public UserProfile? UserProfiles { get; set; }
        public string? Avatar { get; set; }
        public string? Fullname { get; set; }
    }
}
