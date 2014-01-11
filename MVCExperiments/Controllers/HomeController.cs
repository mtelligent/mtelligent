using Mtelligent.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCExperiments.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string post)
        {
            //Someone Clicked the button.
            ExperimentManager.Current.AddConversion("Honey vs Vinegar");

            if (ExperimentManager.Current.GetHypothesis("Honey vs Vinegar").SystemName == "Honey")
            {
                ViewBag.Message = "Thanks you so much you wonderful user.";
            }
            else
            {
                ViewBag.Message = "We knew you would do as you were told. Carry on worm.";
            }

            return View();
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
