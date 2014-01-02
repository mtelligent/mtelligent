using Mtelligent.Dashboard.Data;
using Mtelpligent.Dashboard.Web.ViewModels;
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
        private ICohortRepository cohortRepository;

        public ExperimentsController(IExperimentRepository experimentRepository, ICohortRepository cohortRepository)
        {
            this.experimentRepository = experimentRepository;
            this.cohortRepository = cohortRepository;
        }

        //
        // GET: /Experiments/
        public ActionResult Index()
        {
            var experiments = experimentRepository.GetAll();
            return View(experiments);
        }


        public ActionResult Add()
        {
            ExperimentViewModel viewModel = new ExperimentViewModel();
            viewModel.Experiment = null;
            viewModel.Cohorts = cohortRepository.GetAll().ToList();
            return View(viewModel);
        }

        public ActionResult Update(int Id)
        {
            ExperimentViewModel viewModel = new ExperimentViewModel();
            viewModel.Experiment = experimentRepository.Get(Id);
            viewModel.Cohorts = cohortRepository.GetAll().ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ExperimentViewModel viewModel, int hidNumVariables)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.Experiment.CreatedBy = HttpContext.User.Identity.Name;
                    viewModel.Experiment.Variables = new List<string>();

                    for (int i = 0; i < hidNumVariables; i++)
                    {
                        var variable = Request["variable-" + i];
                        if (!string.IsNullOrEmpty(variable))
                        {
                            viewModel.Experiment.Variables.Add(variable);
                        }
                    }

                    viewModel.Experiment = experimentRepository.Add(viewModel.Experiment);
                    return Redirect("/Experiments/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your responses.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that experiment.";
            }

            viewModel.Cohorts = cohortRepository.GetAll().ToList();
            return View(viewModel);
        }
    }
}
