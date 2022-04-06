using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using INTEX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace INTEX.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ICrashRepository _repo { get; set; }
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, ICrashRepository temp)
        {
            _logger = logger;
            _repo = temp;
        }

        [HttpGet]
        public IActionResult AddCrash()
        {
            ViewBag.Severities = _repo.Severity.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddCrash(Crash crash)
        {
            _repo.AddCrash(crash);
            return RedirectToAction("CrashInfo", new { Controller = "Home" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
