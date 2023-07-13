using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSvc.Application.DTO
{
    public class EventUpdateDTO
    {
        public Guid? MeetingId { get; set; }
        public string? Title { get; set; }
        public string? Background { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? Quantity { get; set; }
    }

    public class EventDTO
    {
        public Guid? Id { get; set; }
        public Guid? MeetingId { get; set; }
        public bool Checked { get; set; }
        public string? Title { get; set; }
        public string? Background { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? Quantity { get; set; }
    }

    public class EventJoinDTO
    {
        public string UserId { get; set; }
        public Guid EventId { get; set; }
        public string? EventTitle { get; set; }
        public string? Avatar { get; set; }
        public string? Fullname { get; set; }
        public string? CheckedInLocation { get; set; }
        public DateTime? CheckedInDate { get; set; }
        public EventDTO Events { get; set; }
    }
}
