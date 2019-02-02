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
                && t.StartTime > DateTime.Now);
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
            var lastWindow = _context.TicketWindows
                .Where(w => w.ConventionId == conConfig.ConventionId)
                .OrderBy(w => w.StartTime)
                .LastOrDefault();

            var newStartTime = lastWindow.StartTime.Add(conConfig.TicketWindowInterval);
            while (newStartTime < conConfig.RegistrationHours.Max(x => x.EndTime)
                && !conConfig.RegistrationHours.Any(h => h.StartTime <= newStartTime 
                    && h.EndTime >= newStartTime.Add(conConfig.TicketWindowInterval)))
            {
                newStartTime = newStartTime.Add(conConfig.TicketWindowInterval);
            }
            if (newStartTime < conConfig.RegistrationHours.Max(x => x.EndTime))
            {
                throw new OperationCanceledException("Unable to create new TicketWindow: no remaining time slots");
            }

            var newWindow = new TicketWindow()
            {
                TicketWindowId = Guid.NewGuid(),
                ConventionId = conConfig.ConventionId,
                StartTime = newStartTime,
                Length = conConfig.TicketWindowInterval,
                AttendeeTickets = new List<AttendeeTicket>(),
                AttendeeCapacity = conConfig.TicketWindowCapacity
            };
        }
    }
}
