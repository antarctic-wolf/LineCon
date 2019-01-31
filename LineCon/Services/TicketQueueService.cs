using System;
using System.Linq;
using System.Threading.Tasks;
using LineCon.Data.Exceptions;
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
        public async Task<TicketWindow> Enqueue(ConConfig conConfig, Attendee attendee, TicketWindow ticketWindow = null)
        {
            //get the ticket for this attendee if one isn't specified
            if (ticketWindow == null)
            {
                ticketWindow = await _ticketWindowService.GetNextAvailable(conConfig);
            }

            //ensure the ticket isn't full
            if (!ticketWindow.Available)
            {
                //we should never get here if the ticket came from _ticketWindowService
                //but it doesn't hurt to check, and this order makes things a bit more readable
                throw new TicketWindowFullException(ticketWindow);
            }

            //if we get here, it's safe to dequeue the attendee in case they're already in line
            await Dequeue(attendee);

            //assign their spot
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
            //it's ok if they were never in line to begin with, this will just do nothing
            var attendeeTickets = _context.AttendeeTickets.Where(t => t.Attendee.AttendeeId == attendee.AttendeeId);
            foreach (var ticket in attendeeTickets)
            {
                ticket.Completed = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
