using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineCon.Models
{
    public class Convention
    {
        [Key]
        public Guid ConventionId { get; set; }

        public Guid ConConfigId { get; set; }
        public virtual ConConfig ConConfig { get; set; }

        public string UrlIdentifier { get; set; }

        public ICollection<ConfirmationNumber> ConfirmationNumbers { get; set; }

        public ICollection<Attendee> Attendees { get; set; }

        public IList<TicketWindow> TicketWindows { get; set; }

        public ICollection<AttendeeTicket> AttendeeTickets { get; set; } //TODO: redundant with this.TicketWindows.AttendeeTickets
    }
}
