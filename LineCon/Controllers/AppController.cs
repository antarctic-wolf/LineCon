using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCon.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index(string conIdentifier)
        {
            return View();
        }
    }
}
