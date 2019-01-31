using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Models
{
    public class ConfirmationNumber
    {
        [Key]
        public Guid ConConfigId { get; set; }

        [ForeignKey("Convention")]
        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }

        public string Number { get; set; }
    }
}
