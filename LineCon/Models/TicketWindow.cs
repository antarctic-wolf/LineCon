using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LineCon.Models
{
    public class TicketWindow
    {
        [Key]
        public Guid TicketWindowId { get; set; }

        [ForeignKey("Convention")]
        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }

        public DateTime StartTime { get; set; }
        public TimeSpan Length { get; set; }

        public List<AttendeeTicket> AttendeeTickets { get; set; }
        public int AttendeeCapacity { get; set; }

        public bool Available => AttendeeTickets.Count(t => !t.Completed) < AttendeeCapacity;
    }
}
