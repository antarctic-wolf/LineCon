using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineCon.Models
{
    public class TicketWindow
    {
        [Key]
        public Guid TicketWindowId { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan Length { get; set; }

        public List<Attendee> Attendees { get; set; }
    }
}
