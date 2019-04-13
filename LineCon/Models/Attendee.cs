using System;
using System.ComponentModel.DataAnnotations;

namespace LineCon.Models
{
    public class Attendee
    {
        [Key]
        public Guid AttendeeId { get; set; }

        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }

        [Required]
        public string ConfirmationNumber { get; set; }

        [Required]
        public string BadgeName { get; set; }
    }
}
