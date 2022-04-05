using INTEX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace INTEX.Controllers
{
    // Only allows authorized users to use controller - option, use another controller for authorized material
    //[Authorize]
    public class HomeController : Controller
    {
        private ICrashRepository _repo { get; set; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICrashRepository temp)
        {
            _logger = logger;
            _repo = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CrashInfo()
        {
            ViewBag.crashes = _repo.Utah_Crashes.Take(10).ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
