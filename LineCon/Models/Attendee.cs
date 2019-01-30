using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineCon.Models
{
    public class Attendee
    {
        [Key]
        public Guid AttendeeId { get; set; }

        public string ConfirmationNumber { get; set; }
        public string BadgeName { get; set; }

        [ForeignKey("TicketWindow")]
        public Guid TicketWindowId { get; set; }
        public TicketWindow TicketWindow { get; set; }
    }
}
