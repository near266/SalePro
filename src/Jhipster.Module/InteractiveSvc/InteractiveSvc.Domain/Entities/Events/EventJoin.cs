using InteractiveSvc.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Domain.Entities.Events
{
    public class EventJoin : BaseEntity<Guid>
    {
        [ForeignKey("UserProfiles")]
        public string UserId { get; set; }
        public UserProfile? UserProfiles { get; set; }
        [ForeignKey("Events")]
        public Guid EventId { get; set; }
        public Event? Events { get; set; }
        public string? EventTitle { get; set; }
        public string? Avatar { get; set; }
        public string? Fullname { get; set; }
        public string? CheckedInLocation { get; set; }
        public DateTime? CheckedInDate { get; set; }
    }
}
