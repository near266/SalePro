using InteractiveSvc.Domain.Entities.Meetings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Domain.Entities.Events
{
    public class Event : BaseEntity<Guid>
    {
        [ForeignKey("Meetings")]
        public Guid? MeetingId { get; set; }
        public Meeting? Meetings { get; set; }
        public string? Title { get; set; }
        public string? Background { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }  
        public int? Status { get; set; }
        public int? Quantity { get; set; }
    }
}
