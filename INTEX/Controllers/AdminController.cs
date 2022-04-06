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
            return View("CrashForm");
        }

        [HttpPost]
        public IActionResult AddCrash(Crash crash)
        {
            _repo.AddCrash(crash);
            int crashId = crash.CRASH_ID;

            return RedirectToAction("CrashDetails", new { Controller = "Home", id = crashId });
        }

        [HttpGet]
        public IActionResult EditCrash(int id)
        {
            ViewBag.Severities = _repo.Severity.ToList();
            var crash = _repo.Utah_Crashes.FirstOrDefault(x => x.CRASH_ID == id);

            return View("CrashForm", crash);
        }

        [HttpPost]
        public IActionResult EditCrash(Crash crash)
        {
            _repo.EditCrash(crash);
            int crashId = crash.CRASH_ID;

            return RedirectToAction("CrashDetails", new { Controller = "Home", id = crashId });
        }

        [HttpGet]
        public IActionResult DeleteCrash(int id)
        {
            Crash crash = _repo.Utah_Crashes.FirstOrDefault(x => x.CRASH_ID == id);
            return View(crash);
        }

        [HttpPost]
        public IActionResult DeleteCrash(Crash crash)
        {
            _repo.RemoveCrash(crash);
            return RedirectToAction("CrashInfo", new { Controller = "Home" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
