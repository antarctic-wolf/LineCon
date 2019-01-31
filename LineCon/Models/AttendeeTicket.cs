using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineCon.Models
{
    public class AttendeeTicket
    {
        [Key]
        public Guid AttendeeTicketId { get; set; }

        public bool Completed { get; set; }

        [ForeignKey("Attendee")]
        public Guid AttendeeId { get; set; }
        public virtual Attendee Attendee { get; set; }

        [ForeignKey("TicketWindow")]
        public Guid TicketWindowId { get; set; }
        public virtual TicketWindow TicketWindow { get; set; }

        [ForeignKey("Convention")]
        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }
    }
}
