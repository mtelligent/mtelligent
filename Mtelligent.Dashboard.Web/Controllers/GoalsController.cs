using Mtelligent.Dashboard.Data;
using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mtelligent.Dashboard.Web.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        private IGoalRepository goalRepository;

        public GoalsController(IGoalRepository goalRepository)
        {
            this.goalRepository = goalRepository;
        }

        public ActionResult Index()
        {
            var goals = goalRepository.GetAll();
            return View(goals);
        }

        public ActionResult Add()
        {
            var goal = new Goal();
            ViewBag.ActionName = "Add";
            ViewBag.Title = "Add a Goal";
            return View("EditGoal", goal);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Goal goal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    goal.CreatedBy = HttpContext.User.Identity.Name;
                    goal = goalRepository.Add(goal);
                    return Redirect("/Goals/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your response.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that goal.";
            }

            
            ViewBag.ActionName = "Add";
            ViewBag.Title = "Add a Goal";
            return View("EditGoal", goal);
        }

        public ActionResult Update(int Id)
        {
            var goal = goalRepository.Get(Id);
            ViewBag.ActionName = "Update";
            ViewBag.Title = "Edit Goal";
            return View("EditGoal", goal);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Goal goal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    goal.UpdatedBy = HttpContext.User.Identity.Name;
                    goal = goalRepository.Update(goal);
                    return Redirect("/Goals/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your response.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that goal.";
            }

            
            ViewBag.ActionName = "Update";
            ViewBag.Title = "Edit Goal";
            return View("EditGoal", goal);
        }

    }
}
