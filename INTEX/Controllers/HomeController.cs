using INTEX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Amazon.Lambda;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using INTEX.Models.ViewModels;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

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

        public IActionResult CrashInfo(int pageNum = 1)
        {
            int pageSize = 20;

            ViewBag.PageInfo = new PageInfo
            {
                TotalNumCrashes = _repo.Utah_Crashes.Count(),
                CrashesPerPage = pageSize,
                CurrentPage = pageNum
            };

            ViewBag.crashes = _repo.Utah_Crashes.Skip((pageNum - 1) * pageSize).Take(pageSize);

            return View();
        }

        [HttpGet]
        public IActionResult CrashDetails(int id)
        {
            var crash = _repo.Utah_Crashes.FirstOrDefault(x => x.CRASH_ID == id);

            return View(crash);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
