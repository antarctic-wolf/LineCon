using LineCon.Data;
using LineCon.Models;
using LineCon.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LineCon.Controllers
{
    public class ApiController : Controller
    {
        private readonly LineConContext _context;
        private readonly IRegistrationService _registrationService;
        private readonly ITicketQueueService _ticketQueueService;

        public ApiController(
            LineConContext context,
            IRegistrationService registrationService,
            ITicketQueueService ticketQueueService)
        {
            _context = context;
            _registrationService = registrationService;
            _ticketQueueService = ticketQueueService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewAttendee attendee)
        {
            try
            {
                await _registrationService.Register(attendee);
                return Ok();
            }
            catch(AttendeeExistsException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Enqueue(Guid attendeeId)
        {
            var attendee = _context.Attendees.SingleOrDefault(a => a.AttendeeId == attendeeId);
            var window = await _ticketQueueService.Enqueue(attendee);
            return Ok(new
            {
                window.StartTime,
                window.Length
            });
        }

        [HttpPut]
        public async Task<IActionResult> Dequeue(Guid attendeeId)
        {
            var attendee = _context.Attendees.SingleOrDefault(a => a.AttendeeId == attendeeId);
            await _ticketQueueService.Dequeue(attendee);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetWindow(Guid attendeeId)
        {
            var ticket = _context.AttendeeTickets.SingleOrDefault(t => t.AttendeeId == attendeeId);
            var window = ticket.TicketWindow;
            return Ok(new
            {
                window.StartTime,
                window.Length
            });
        }
        
        //TODO: jump to back of line
    }
}
