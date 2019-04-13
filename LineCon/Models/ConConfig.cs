using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineCon.Models
{
    public class ConConfig
    {
        [Key]
        public Guid ConConfigId { get; set; }

        public IList<RegistrationHours> RegistrationHours { get; set; }

        public TimeSpan TicketWindowInterval { get; set; }

        public int TicketWindowCapacity { get; set; }

        public bool RequireConfirmationNumber { get; set; }
    }
}
