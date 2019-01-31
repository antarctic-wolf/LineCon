using LineCon.Data;
using LineCon.Data.Exceptions;
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
        private readonly ITicketWindowService _ticketWindowService;

        public ApiController(
            LineConContext context,
            IRegistrationService registrationService,
            ITicketQueueService ticketQueueService,
            ITicketWindowService ticketWindowService)
        {
            _context = context;
            _registrationService = registrationService;
            _ticketQueueService = ticketQueueService;
            _ticketWindowService = ticketWindowService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(NewAttendee attendee)
        {
            var convention = _context.Conventions.SingleOrDefault(c => c.ConventionId == attendee.ConventionId);
            if (convention == null)
            {
                return StatusCode(500, $"No convention found with this id: {attendee.ConventionId}");
            }

            if (convention.ConConfig.RequireConfirmationNumber
                && !convention.ConfirmationNumbers.Any(n => n.Number == attendee.ConfirmationNumber))
            {
                return Forbid($"No registration found with this confirmation number: {attendee.ConfirmationNumber}");
            }

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
            var (attendee, conConfig) = GetAttendee(attendeeId);
            var window = await _ticketQueueService.Enqueue(attendee);
            return Ok(new
            {
                window.StartTime,
                window.Length
            });
        }

        [HttpPost]
        public async Task<IActionResult> Enqueue(Guid attendeeId, Guid ticketWindowId)
        {
            var (attendee, conConfig) = GetAttendee(attendeeId);
            var ticketWindow = _context.TicketWindows.SingleOrDefault(w => w.TicketWindowId == ticketWindowId);
            await _ticketQueueService.Enqueue(attendee, ticketWindow); //TODO: do we want to catch the TicketWindowFullException?
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Dequeue(Guid attendeeId)
        {
            var (attendee, conConfig) = GetAttendee(attendeeId);
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
        
        [HttpGet]
        public async Task<IActionResult> GetAvailableWindows()
        {
            var windows = _ticketWindowService.GetAllAvailable().Select(window => new
            {
                window.StartTime,
                window.Length
            });
            return Ok(windows);
        }

        private (Attendee, ConConfig) GetAttendee(Guid attendeeId)
        {
            var attendee = _context.Attendees.SingleOrDefault(a => a.AttendeeId == attendeeId);
            var conConfig = _context.Conventions.SingleOrDefault(c => c.ConConfigId == attendee.ConventionId).ConConfig;
            return (attendee, conConfig);
        }
    }
}
