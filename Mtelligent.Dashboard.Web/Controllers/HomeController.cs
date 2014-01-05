using Mtelligent.Dashboard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mtelligent.Dashboard.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IDashboardRepository dashboardRepository = null;

        public HomeController(IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
        }

        public ActionResult Index()
        {
            var summary = dashboardRepository.GetExperimentStatuses();
            return View(summary);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
