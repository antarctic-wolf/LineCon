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
        public async Task<IActionResult> Kiosk()
        {
            var convention = HttpContext.Items["convention"] as Convention;

            //TODO
            return View();
        }

        /// <summary>
        /// This is the registration page for the attendees
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var convention = HttpContext.Items["convention"] as Convention;

            var model = new IndexViewModel()
            {
                RequireConfirmationNumber = convention.ConConfig.RequireConfirmationNumber,
                AailableWindows = (await _ticketWindowService.GetAllAvailable(convention.ConventionId)).OrderBy(w => w.StartTime),
                NewAttendee = new NewAttendee()
                {
                    ConventionId = convention.ConventionId
                }
            };
            return View(model);
        }

        /// <summary>
        /// This handles the attendee registration
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            var convention = HttpContext.Items["convention"] as Convention;

            if (convention.ConConfig.RequireConfirmationNumber
                && !convention.ConfirmationNumbers.Any(n => n.Number == model.NewAttendee.ConfirmationNumber))
            {
                return Forbid($"No registration found with this confirmation number: {model.NewAttendee.ConfirmationNumber}");
            }

            try
            {
                //register them with LineCon
                var attendee = await _registrationService.Register(model.NewAttendee);

                //put them in line
                //if they didn't select a window, it'll pass null which is ok. It'll then select the next available
                var window = await _ticketQueueService.Enqueue(attendee, model.SelectedWindow);

                return Ok(); //TODO inform them of their time
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
