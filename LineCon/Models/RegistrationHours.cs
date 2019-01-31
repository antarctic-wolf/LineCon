using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Models
{
    public class RegistrationHours
    {
        [Key]
        public Guid RegistrationHoursId { get; set; }

        [ForeignKey("ConConfig")]
        public Guid ConConfigId { get; set; }
        public virtual ConConfig ConConfig { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
