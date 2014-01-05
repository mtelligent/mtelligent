using Mtelligent.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mtelligent.Dashboard.Web.Controllers
{
    public class SamplesController : Controller
    {
        //
        // GET: /Samples/

        public ActionResult Index()
        {
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

    }
}
