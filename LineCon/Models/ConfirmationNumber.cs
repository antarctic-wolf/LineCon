using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LineCon.Models
{
    public class ConfirmationNumber
    {
        [Key]
        public Guid ConConfigId { get; set; }

        public Guid ConventionId { get; set; }
        public virtual Convention Convention { get; set; }

        public string Number { get; set; }
    }
}
