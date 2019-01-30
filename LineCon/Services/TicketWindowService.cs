using System;
using System.Linq;
using System.Threading.Tasks;
using LineCon.Models;

namespace LineCon.Services
{
    public class TicketWindowService : ITicketWindowService
    {
        private readonly LineConContext _context;

        public TicketWindowService(LineConContext context)
        {
            _context = context;
        }

        public TicketWindow GetNextAvailable()
        {
            return _context.TicketWindows.Where(t => t.StartTime > DateTime.Now)
                .OrderBy(t => t.StartTime)
                .FirstOrDefault(t => t.AttendeeTickets.Count(x => !x.Completed) < t.AttendeeCapacity);
        }

        public async Task<TicketWindow> Create()
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
