using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineCon.Models
{
    public class ConConfig
    {
        [Key]
        public Guid ConConfigId { get; set; }

        [ForeignKey("Convention")]
        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }

        public List<RegistrationHours> RegistrationHours { get; set; }

        public TimeSpan TicketWindowInterval { get; set; }

        public int TicketWindowCapacity { get; set; }

        public bool RequireConfirmationNumber { get; set; }
    }
}
