using Microsoft.EntityFrameworkCore;

namespace LineCon.Models
{
    public class LineConContext : DbContext
    {
        public LineConContext(DbContextOptions<LineConContext> options)
            : base(options)
        { }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<TicketWindow> TicketWindows { get; set; }
        public DbSet<AttendeeTicket> AttendeeTickets { get; set; }
    }
}
