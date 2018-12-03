using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Models
{
    public class LineConContext : DbContext
    {
        public LineConContext(DbContextOptions<LineConContext> options)
            : base(options)
        { }

        public DbSet<Attendee> Attendees { get; set;  }
    }
}
