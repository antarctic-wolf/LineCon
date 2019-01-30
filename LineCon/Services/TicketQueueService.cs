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

        public async Task<TicketWindow> Enqueue(Attendee attendee)
        {
            //TODO: handle case where attendee is already queued

            var ticketWindow = _ticketWindowService.GetNextAvailable();
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

        public async Task Dequeue(Attendee attendee)
        {
            var attendeeTicket = _context.AttendeeTickets.SingleOrDefault(t => t.Attendee.AttendeeId == attendee.AttendeeId);
            attendeeTicket.Completed = true;

            await _context.SaveChangesAsync();
        }
    }
}
