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
using Microsoft.EntityFrameworkCore;

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

        [Route("CrashInfo/{pageNum:int?}")]
        [HttpGet]
        public IActionResult CrashInfo(int pageNum = 1)
        {
            ViewBag.post = false;
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

        [Route("CrashInfo/{pageNum:int}")]
        [HttpPost]
        public IActionResult CrashInfo(Filter f, int pageNum = 1)
        {
            ViewBag.post = true;
            int pageSize = 20;

            var crashes = _repo.Utah_Crashes;

            if (f.CRASH_DATETIME != null)
            {
                var date = DateTime.Parse(f.CRASH_DATETIME);
                crashes = crashes.Where(x => x.CRASH_DATETIME.Date == date);
            }
            if (f.MAIN_ROAD_NAME != null)
            {
                crashes = crashes.Where(x => x.MAIN_ROAD_NAME.Contains(f.MAIN_ROAD_NAME));
            }
            if (f.BICYCLIST_INVOLVED)
            {
                crashes = crashes.Where(x => x.BICYCLIST_INVOLVED);
            }
            if(f.COMMERCIAL_MOTOR_VEH_INVOLVED)
            {
                crashes = crashes.Where(x => x.COMMERCIAL_MOTOR_VEH_INVOLVED);
            }
            if (!f.CRASH_SEVERITY_1 && !f.CRASH_SEVERITY_2 && !f.CRASH_SEVERITY_3 && !f.CRASH_SEVERITY_4 && !f.CRASH_SEVERITY_5) { }
            else
            {
                if (!f.CRASH_SEVERITY_1)
                {
                    crashes = crashes.Where(x => x.CRASH_SEVERITY_ID != 1);
                }
                if (!f.CRASH_SEVERITY_2)
                {
                    crashes = crashes.Where(x => x.CRASH_SEVERITY_ID != 2);
                }
                if (!f.CRASH_SEVERITY_3)
                {
                    crashes = crashes.Where(x => x.CRASH_SEVERITY_ID != 3);
                }
                if (!f.CRASH_SEVERITY_4)
                {
                    crashes = crashes.Where(x => x.CRASH_SEVERITY_ID != 4);
                }
                if (!f.CRASH_SEVERITY_5)
                {
                    crashes = crashes.Where(x => x.CRASH_SEVERITY_ID != 5);
                }
            }
            if (f.DISTRACTED_DRIVING)
            {
                crashes = crashes.Where(x => x.DISTRACTED_DRIVING);
            }
            if (f.DOMESTIC_ANIMAL_RELATED)
            {
                crashes = crashes.Where(x => x.DOMESTIC_ANIMAL_RELATED);
            }
            if (f.DROWSY_DRIVING)
            {
                crashes = crashes.Where(x => x.DROWSY_DRIVING);
            }
            if (f.DUI)
            {
                crashes = crashes.Where(x => x.DUI);
            }
            if (f.IMPROPER_RESTRAINT)
            {
                crashes = crashes.Where(x => x.IMPROPER_RESTRAINT);
            }
            if (f.INTERSECTION_RELATED)
            {
                crashes = crashes.Where(x => x.INTERSECTION_RELATED);
            }
            if (f.MOTORCYCLE_INVOLVED)
            {
                crashes = crashes.Where(x => x.MOTORCYCLE_INVOLVED);
            }
            if (f.NIGHT_DARK_CONDITION)
            {
                crashes = crashes.Where(x => x.NIGHT_DARK_CONDITION);
            }
            if (f.OLDER_DRIVER_INVOLVED)
            {
                crashes = crashes.Where(x => x.OLDER_DRIVER_INVOLVED);
            }
            if (f.OVERTURN_ROLLOVER)
            {
                crashes = crashes.Where(x => x.OVERTURN_ROLLOVER);
            }
            if (f.PEDESTRIAN_INVOLVED)
            {
                crashes = crashes.Where(x => x.PEDESTRIAN_INVOLVED);
            }
            if (f.ROADWAY_DEPARTURE)
            {
                crashes = crashes.Where(x => x.ROADWAY_DEPARTURE);
            }
            if (f.SINGLE_VEHICLE)
            {
                crashes = crashes.Where(x => x.SINGLE_VEHICLE);
            }
            if (f.TEENAGE_DRIVER_INVOLVED)
            {
                crashes = crashes.Where(x => x.TEENAGE_DRIVER_INVOLVED);
            }
            if (f.UNRESTRAINED)
            {
                crashes = crashes.Where(x => x.UNRESTRAINED);
            }
            if (f.WILD_ANIMAL_RELATED)
            {
                crashes = crashes.Where(x => x.WILD_ANIMAL_RELATED);
            }
            if (f.WORK_ZONE_RELATED)
            {
                crashes = crashes.Where(x => x.WORK_ZONE_RELATED);
            }

            ViewBag.PageInfo = new PageInfo
            {
                TotalNumCrashes = crashes.Count(),
                CrashesPerPage = pageSize,
                CurrentPage = pageNum
            };

            ViewBag.crashes = crashes.Skip((pageNum - 1) * pageSize).Take(pageSize);

            return View();
        }

        [HttpGet]
        public IActionResult CrashDetails(int id)
        {
            var crash = _repo.Utah_Crashes.FirstOrDefault(x => x.CRASH_ID == id);
            ViewBag.Severities = _repo.Severity.ToList();
            return View(crash);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
