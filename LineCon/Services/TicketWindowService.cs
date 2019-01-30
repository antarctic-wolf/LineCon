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

        /// <summary>
        /// Gets the next available TicketWindow
        /// </summary>
        /// <returns></returns>
        public TicketWindow GetNextAvailable()
        {
            return _context.TicketWindows.Where(t => t.StartTime > DateTime.Now)
                .OrderBy(t => t.StartTime)
                .FirstOrDefault(t => t.AttendeeTickets.Count(x => !x.Completed) < t.AttendeeCapacity);
        }

        /// <summary>
        /// Creates a new TicketWindow
        /// </summary>
        /// <returns></returns>
        public async Task<TicketWindow> Create()
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
