using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Models
{
    public class Convention
    {
        [Key]
        public Guid ConventionId { get; set; }

        [ForeignKey("ConConfig")]
        public Guid ConConfigId { get; set; }
        public virtual ConConfig ConConfig { get; set; }

        public List<ConfirmationNumber> ConfirmationNumbers { get; set; }

        public List<Attendee> Attendees { get; set; }

        public List<TicketWindow> TicketWindows { get; set; }

        public List<AttendeeTicket> AttendeeTickets { get; set; }
    }
}
