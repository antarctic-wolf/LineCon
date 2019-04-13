using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LineCon.Models;
using Microsoft.EntityFrameworkCore;

namespace LineCon.Services
{
    public interface ITicketWindowService
    {
        Task<IEnumerable<TicketWindow>> GetAllAvailable(Guid conventionId);
        Task<TicketWindow> GetNextAvailable(Guid conventionId);
        Task<TicketWindow> Create(Guid conventionId);
    }

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
        /// <param name="conventionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TicketWindow>> GetAllAvailable(Guid conventionId)
        {
            return await _context.TicketWindows
                .Where(t => t.ConventionId == conventionId
                    && t.Available
                    && t.StartTime >= DateTime.Now)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the next available TicketWindow
        /// </summary>
        /// <param name="conventionId"></param>
        /// <returns></returns>
        public async Task<TicketWindow> GetNextAvailable(Guid conventionId)
        {
            var window = (await GetAllAvailable(conventionId))
                .OrderBy(t => t.StartTime)
                .FirstOrDefault();
            if (window == null)
            {
                window = await Create(conventionId);
            }
            return window;
        }

        /// <summary>
        /// Creates a new TicketWindow
        /// </summary>
        /// <param name="conventionId"></param>
        /// <returns></returns>
        public async Task<TicketWindow> Create(Guid conventionId)
        {
            var conConfig = (await _context.Conventions
                .Include(c => c.ConConfig)
                    .ThenInclude(cc => cc.RegistrationHours)
                .SingleOrDefaultAsync(c => c.ConventionId == conventionId))
                .ConConfig;

            var lastWindow = await _context.TicketWindows
                .Where(w => w.ConventionId == conventionId)
                .OrderBy(w => w.StartTime)
                .LastOrDefaultAsync();

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
                ConventionId = conventionId,
                StartTime = newStartTime,
                Length = conConfig.TicketWindowInterval,
                AttendeeTickets = new List<AttendeeTicket>(),
                AttendeeCapacity = conConfig.TicketWindowCapacity
            };

            _context.TicketWindows.Add(newWindow);
            await _context.SaveChangesAsync();
            return newWindow;
        }
    }
}
