using System;
using System.Linq;
using System.Threading.Tasks;
using LineCon.Models;

namespace LineCon.Services
{
    public class TicketQueueService : ITicketQueueService
    {
        private readonly LineConContext _context;
        private readonly ITicketWindowService _ticketWindowService;

        public TicketQueueService(LineConContext context, ITicketWindowService ticketWindowService)
        {
            _context = context;
            _ticketWindowService = ticketWindowService;
        }

        /// <summary>
        /// Adds an Attendee to the ticket queue in the next available window
        /// </summary>
        /// <param name="attendee"></param>
        /// <returns></returns>
        public async Task<TicketWindow> Enqueue(Attendee attendee, TicketWindow ticketWindow = null)
        {
            //TODO: handle given ticketWindow
            //TODO: handle case where attendee is already queued

            ticketWindow = _ticketWindowService.GetNextAvailable();
            if (ticketWindow == null)
            {
                ticketWindow = await _ticketWindowService.Create();
            }

            var attendeeTicket = new AttendeeTicket()
            {
                AttendeeTicketId = Guid.NewGuid(),
                Attendee = attendee,
                TicketWindow = ticketWindow
            };
            _context.AttendeeTickets.Add(attendeeTicket);

            ticketWindow.AttendeeTickets.Add(attendeeTicket);

            await _context.SaveChangesAsync();

            return ticketWindow;
        }

        /// <summary>
        /// Removes an Attendee from the ticket queue by marking their ticket as complete
        /// </summary>
        /// <param name="attendee"></param>
        /// <returns></returns>
        public async Task Dequeue(Attendee attendee)
        {
            var attendeeTicket = _context.AttendeeTickets.SingleOrDefault(t => t.Attendee.AttendeeId == attendee.AttendeeId);
            attendeeTicket.Completed = true;

            await _context.SaveChangesAsync();
        }
    }
}
