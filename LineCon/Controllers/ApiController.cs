using LineCon.Data;
using LineCon.Data.Exceptions;
using LineCon.Models;
using LineCon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Enqueue(Guid attendeeId)
        {
            var (attendee, conConfig) = await GetAttendee(attendeeId);
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
            var (attendee, conConfig) = await GetAttendee(attendeeId);
            var ticketWindow = await _context.TicketWindows.SingleOrDefaultAsync(w => w.TicketWindowId == ticketWindowId);
            try
            {
                await _ticketQueueService.Enqueue(attendee, ticketWindow);
                return Ok();
            }
            catch (TicketWindowFullException e)
            {
                return Forbid(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Dequeue(Guid attendeeId)
        {
            var (attendee, conConfig) = await GetAttendee(attendeeId);
            await _ticketQueueService.Dequeue(attendee);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetWindow(Guid attendeeId)
        {
            var ticket =await _context.AttendeeTickets.SingleOrDefaultAsync(t => t.AttendeeId == attendeeId);
            var window = ticket.TicketWindow;
            return Ok(new
            {
                window.StartTime,
                window.Length
            });
        }
        
        //[HttpGet]
        //public async Task<IActionResult> GetAvailableWindows()
        //{
        //    var windows = _ticketWindowService.GetAllAvailable().Select(window => new
        //    {
        //        window.StartTime,
        //        window.Length
        //    });
        //    return Ok(windows);
        //}

        private async Task<(Attendee, ConConfig)> GetAttendee(Guid attendeeId)
        {
            var attendee = await _context.Attendees.SingleOrDefaultAsync(a => a.AttendeeId == attendeeId);
            var conConfig = (await _context.Conventions.SingleOrDefaultAsync(c => c.ConConfigId == attendee.ConventionId)).ConConfig;
            return (attendee, conConfig);
        }
    }
}
