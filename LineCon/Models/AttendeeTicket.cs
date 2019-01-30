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
        public Attendee Attendee { get; set; }

        [ForeignKey("TicketWindow")]
        public Guid TicketWindowId { get; set; }
        public TicketWindow TicketWindow { get; set; }
    }
}
