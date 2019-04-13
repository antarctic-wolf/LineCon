using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineCon.Models
{
    public class RegistrationHours
    {
        [Key]
        public Guid RegistrationHoursId { get; set; }

        public Guid ConConfigId { get; set; }
        public virtual ConConfig ConConfig { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
