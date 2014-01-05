using Mtelligent.Dashboard.Data;
using Mtelligent.Dashboard.Web.ViewModels;
using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mtelligent.Dashboard.Web.Controllers
{
    [Authorize]
    public class ExperimentsController : Controller
    {
        private IExperimentRepository experimentRepository;
        private ICohortRepository cohortRepository;
        private IGoalRepository goalRepository;

        public ExperimentsController(IExperimentRepository experimentRepository, ICohortRepository cohortRepository, IGoalRepository goalRepository)
        {
            this.experimentRepository = experimentRepository;
            this.cohortRepository = cohortRepository;
            this.goalRepository = goalRepository;
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
            viewModel.Goals = goalRepository.GetAll().ToList();
            return View(viewModel);
        }

        public ActionResult Update(int Id)
        {
            ExperimentViewModel viewModel = new ExperimentViewModel();
            viewModel.Experiment = experimentRepository.Get(Id);
            viewModel.Cohorts = cohortRepository.GetAll().ToList();
            viewModel.Goals = goalRepository.GetAll().ToList();
            return View(viewModel);
        }

        public ActionResult AddSegment(int Id)
        {
            var viewModel = new SegmentViewModel();

            var experiment = experimentRepository.Get(Id);
            viewModel.ExperimentName = experiment.Name;
            viewModel.Variables = experiment.Variables;
            viewModel.ExperimentId = experiment.Id;

            return View(viewModel);
        }

        public ActionResult UpdateSegment(int experimentId, int segmentId)
        {
            var viewModel = new SegmentViewModel();

            var experiment = experimentRepository.Get(experimentId);
            var segment = experiment.Segments.FirstOrDefault(a => a.Id == segmentId);
            viewModel.Segment = segment;    

            viewModel.ExperimentName = experiment.Name;
            viewModel.Variables = experiment.Variables;
            viewModel.ExperimentId = experiment.Id;

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

        [HttpPost]
        public ActionResult Update(ExperimentViewModel viewModel, int hidNumVariables)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.Experiment.UpdatedBy = HttpContext.User.Identity.Name;
                    viewModel.Experiment.Variables = new List<string>();

                    for (int i = 0; i < hidNumVariables; i++)
                    {
                        var variable = Request["variable-" + i];
                        if (!string.IsNullOrEmpty(variable))
                        {
                            viewModel.Experiment.Variables.Add(variable);
                        }
                    }

                    viewModel.Experiment = experimentRepository.Update(viewModel.Experiment);
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

        [HttpPost]
        public ActionResult AddSegment(SegmentViewModel viewModel)
        {
            var experiment = experimentRepository.Get(viewModel.ExperimentId);

            try
            {
                if (ModelState.IsValid)
                {                    

                    viewModel.Segment.CreatedBy = HttpContext.User.Identity.Name;
                    viewModel.Segment.Variables = new Dictionary<string, string>();
                    viewModel.Segment.ExperimentId = viewModel.ExperimentId;

                    foreach(var variable in experiment.Variables)
                    {
                        var varValue = Request["prop-" + variable];
                        if (!string.IsNullOrEmpty(varValue))
                        {
                            viewModel.Segment.Variables.Add(variable, varValue);
                        }
                    }

                    viewModel.Segment = experimentRepository.AddSegment(viewModel.Segment);
                    return Redirect("/Experiments/Update?Id=" + experiment.Id);
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your responses.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that segment.";
            }

            
            viewModel.ExperimentName = experiment.Name;
            viewModel.Variables = experiment.Variables;
            viewModel.ExperimentId = experiment.Id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateSegment(SegmentViewModel viewModel)
        {
            var experiment = experimentRepository.Get(viewModel.ExperimentId);

            try
            {
                if (ModelState.IsValid)
                {

                    viewModel.Segment.UpdatedBy = HttpContext.User.Identity.Name;
                    viewModel.Segment.Variables = new Dictionary<string, string>();
                    viewModel.Segment.ExperimentId = viewModel.ExperimentId;

                    foreach (var variable in experiment.Variables)
                    {
                        var varValue = Request["prop-" + variable];
                        if (!string.IsNullOrEmpty(varValue))
                        {
                            viewModel.Segment.Variables.Add(variable, varValue);
                        }
                    }

                    viewModel.Segment = experimentRepository.UpdateSegment(viewModel.Segment);
                    return Redirect("/Experiments/Update?Id=" + experiment.Id);
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your responses.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that segment.";
            }


            viewModel.ExperimentName = experiment.Name;
            viewModel.Variables = experiment.Variables;
            viewModel.ExperimentId = experiment.Id;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateSegments(int experimentId)
        {
            var experiment = experimentRepository.Get(experimentId);
            
            foreach (var segment in experiment.Segments)
            {
                var reqIsDefault = Request[segment.Id + "-IsDefault"];
                var reqTargetPercentage = Request[segment.Id + "-TargetPercentage"];

                if (!string.IsNullOrEmpty(reqIsDefault))
                {
                    segment.IsDefault = bool.Parse(reqIsDefault);
                }
                if (!string.IsNullOrEmpty(reqTargetPercentage))
                {
                    segment.TargetPercentage = double.Parse(reqTargetPercentage);
                }

                experimentRepository.UpdateSegment(segment);
            }

            return Redirect("/Experiments/Update?ID=" + experimentId);
        }
    }
}
