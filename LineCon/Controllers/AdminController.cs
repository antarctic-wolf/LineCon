using LineCon.Models;
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
        [HttpGet]
        public IActionResult Index(string conIdentifier)
        {
            return View(new AdminConsoleViewModel()
            {
                ConConfig = new ConConfigViewModel()
                {
                    ConConfigId = Guid.Empty,
                    ConventionId = Guid.Empty,
                    RegistrationHours = new List<RegistrationHours>()
                    {
                        new RegistrationHours()
                        {
                            RegistrationHoursId = Guid.NewGuid()
                        }
                    },
                    TicketWindowInterval = 5,
                    TicketWindowCapacity = 5,
                    RequireConfirmationNumber = true
                }
            });
        }

        [HttpPost]
        public IActionResult Index([Bind] AdminConsoleViewModel model)
        {
            //TODO
            return View(model);
        }

        [HttpGet]
        public IActionResult AddHours()
        {
            return View(new AdminConsoleViewModel()
            {
                ConConfig = new ConConfigViewModel()
                {
                    RegistrationHours = new List<RegistrationHours>()
                    {
                        new RegistrationHours()
                        {
                            RegistrationHoursId = Guid.NewGuid()
                        }
                    }
                }
            });
        }
    }
}
