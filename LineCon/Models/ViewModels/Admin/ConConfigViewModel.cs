using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineCon.Models.ViewModels.Admin
{
    public class ConConfigViewModel
    {
        public Guid ConConfigId { get; set; }
        public Guid ConventionId { get; set; }

        [Display(Name = "Registration Hours of Operation")]
        public List<Tuple<DateTime, DateTime>> RegistrationHours { get; set; }

        [Display(Name = "Ticket Window Interval")]
        public int TicketWindowInterval { get; set; }

        [Display(Name = "Ticket Window Capacity")]
        public int TicketWindowCapacity { get; set; }

        [Display(Name = "Require Confirmation Number")]
        public bool RequireConfirmationNumber { get; set; }
    }
}
