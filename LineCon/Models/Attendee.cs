using System;

namespace LineCon.Models
{
    public class Attendee
    {
        public int AttendeeId { get; set; }
        public String ConfirmationNumber { get; set; }
        public String BadgeName { get; set; }
        public TicketWindow TicketWindow { get; set; }
    }
}
