using LineCon.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View(new ConConfigViewModel()
            {
                ConConfigId = Guid.Empty,
                ConventionId = Guid.Empty,
                RegistrationHours = new List<Tuple<DateTime, DateTime>>()
                {
                    new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now.AddHours(8))
                },
                TicketWindowInterval = 5,
                TicketWindowCapacity = 5,
                RequireConfirmationNumber = true
            });
        }
    }
}
