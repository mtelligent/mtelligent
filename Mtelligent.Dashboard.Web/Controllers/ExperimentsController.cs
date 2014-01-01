using Mtelligent.Dashboard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mtelligent.Dashboard.Web.Controllers
{
    public class ExperimentsController : Controller
    {
        private IExperimentRepository experimentRepository;

        public ExperimentsController(IExperimentRepository experimentRepository)
        {
            this.experimentRepository = experimentRepository;
        }

        //
        // GET: /Experiments/
        public ActionResult Index()
        {
            var experiments = experimentRepository.GetAll();
            return View(experiments);
        }

    }
}
