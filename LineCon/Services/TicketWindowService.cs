using System;
using System.Collections.Generic;
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
        /// Gets all available TicketWindows
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TicketWindow> GetAllAvailable()
        {
            return _context.TicketWindows.Where(t => t.Available
                && t.StartTime > DateTime.Now); //TODO should this go by end time instead?
        }

        /// <summary>
        /// Gets the next available TicketWindow
        /// </summary>
        /// <returns></returns>
        public async Task<TicketWindow> GetNextAvailable(ConConfig conConfig)
        {
            var window = GetAllAvailable()
                .OrderBy(t => t.StartTime)
                .FirstOrDefault();
            if (window == null)
            {
                window = await Create(conConfig);
            }
            return window;
        }

        /// <summary>
        /// Creates a new TicketWindow
        /// </summary>
        /// <returns></returns>
        public async Task<TicketWindow> Create(ConConfig conConfig)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
