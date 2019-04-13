using System;
using System.ComponentModel.DataAnnotations;

namespace LineCon.Models
{
    public class AttendeeTicket
    {
        [Key]
        public Guid AttendeeTicketId { get; set; }

        public bool Completed { get; set; }

        public Guid AttendeeId { get; set; }
        public virtual Attendee Attendee { get; set; }

        public Guid TicketWindowId { get; set; }
        public virtual TicketWindow TicketWindow { get; set; }

        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }
    }
}
