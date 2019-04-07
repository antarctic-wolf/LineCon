using LineCon.Data;
using LineCon.Data.Exceptions;
using LineCon.Models;
using LineCon.Models.ViewModels.App;
using LineCon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Controllers
{
    public class AppController : Controller
    {
        private readonly LineConContext _context;
        private readonly IRegistrationService _registrationService;
        private readonly ITicketQueueService _ticketQueueService;
        private readonly ITicketWindowService _ticketWindowService;

        public AppController(
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

        /// <summary>
        /// This displays the screen for the kiosk to direct attendees to the registration page
        /// </summary>
        public IActionResult Kiosk(string conIdentifier)
        {
            //TODO
            return View();
        }

        /// <summary>
        /// This is the registration page for the attendees
        /// </summary>
        public async Task<IActionResult> Index(string conIdentifier)
        {
            var con = await _context.Conventions
                .Include(c => c.ConConfig)
                .SingleOrDefaultAsync(c => c.UrlIdentifier.ToString() == conIdentifier.ToString());
            var attendee = new NewAttendee()
            {
                ConventionId = con.ConventionId
            };
            var model = new IndexViewModel()
            {
                RequireConfirmationNumber = con.ConConfig.RequireConfirmationNumber,
                NewAttendee = attendee
            };
            return View(model);
        }

        /// <summary>
        /// This handles the attendee registration
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Index(string conIdentifier, IndexViewModel model)
        {
            var convention = await _context.Conventions
                .SingleOrDefaultAsync(c => c.ConventionId == model.NewAttendee.ConventionId);
            if (convention == null)
            {
                return StatusCode(500, $"No convention found with this id: {model.NewAttendee.ConventionId}");
            }

            if (convention.ConConfig.RequireConfirmationNumber
                && !convention.ConfirmationNumbers.Any(n => n.Number == model.NewAttendee.ConfirmationNumber))
            {
                return Forbid($"No registration found with this confirmation number: {model.NewAttendee.ConfirmationNumber}");
            }

            try
            {
                await _registrationService.Register(model.NewAttendee);
                return Ok();
            }
            catch (AttendeeExistsException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
