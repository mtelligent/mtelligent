using Mtelligent.Configuration;
using Mtelligent.Dashboard.Data;
using Mtelligent.Dashboard.Web.ViewModels;
using Mtelligent.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mtelligent.Dashboard.Web.Controllers
{
    [Authorize]
    public class CohortsController : Controller
    {
        private ICohortRepository cohortRepository;

        public CohortsController(ICohortRepository cohortRepository)
        {
            this.cohortRepository = cohortRepository;
        }

        // GET: /Cohorts/

        public ActionResult Index()
        {
            var cohorts = cohortRepository.GetAll();
            return View(cohorts);
        }

        public ActionResult Add()
        {
            CohortViewModel model = new CohortViewModel();
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            model.CohortTypes = config.Cohorts.ToList().Where(a => a.AllowNew).ToList();
            return View(model);
        }

        public ActionResult Update(int Id)
        {
            CohortViewModel model = new CohortViewModel();

            var cohort = cohortRepository.Get(Id);
            model.Name = cohort.Name;
            model.SystemName = cohort.SystemName;
            model.Id = Id;

            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            model.CohortTypes = config.Cohorts.ToList().Where(a => a.AllowNew).ToList();

            var cohortType = config.Cohorts.ToList().Where(a => a.TypeName == cohort.TypeName).FirstOrDefault();
            model.SelectedCohortType = cohortType.Name;

            model.Properties = new List<CustomCohortPropertyInfo>();

            foreach (var propInfo in cohort.GetType().GetProperties())
            {
                if (Attribute.IsDefined(propInfo, typeof(UserEditableAttribute)))
                {
                    var name = propInfo.Name;

                    if (Attribute.IsDefined(propInfo, typeof(DisplayAttribute)))
                    {
                        var attr = (DisplayAttribute[])propInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                        if (attr.Length > 0)
                        {
                            name = attr[0].Name;
                        }
                    }

                    var required = Attribute.IsDefined(propInfo, typeof(RequiredAttribute));

                    var info = new CustomCohortPropertyInfo()
                    {
                        Name = propInfo.Name,
                        DisplayName = name,
                        Required = required,
                        Value = propInfo.GetValue(cohort).ToString()
                    };

                    model.Properties.Add(info);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(CohortViewModel viewModel)
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");

            try
            {
                if (ModelState.IsValid)
                {
                   
                    var cohortType = config.Cohorts[viewModel.SelectedCohortType];                    

                    Type t = Type.GetType(cohortType.TypeName);
                    Cohort cohort = Activator.CreateInstance(t) as Cohort;
                    cohort.Name = viewModel.Name;
                    cohort.SystemName = viewModel.SystemName;
                    cohort.TypeName = cohortType.TypeName;

                    foreach (var propInfo in t.GetProperties())
                    {
                        if (Attribute.IsDefined(propInfo, typeof(UserEditableAttribute)))
                        {
                            if (Request["prop-" + propInfo.Name] != null)
                            {
                                if (propInfo.PropertyType == typeof(string))
                                {
                                    propInfo.SetValue(cohort, Request["prop-" + propInfo.Name]);
                                }

                                if (propInfo.PropertyType == typeof(DateTime))
                                {
                                    if (Request["prop-" + propInfo.Name] != string.Empty)
                                    {
                                        propInfo.SetValue(cohort, DateTime.Parse(Request["prop-" + propInfo.Name]));
                                    }
                                }
                            }
                        }
                    }

                    cohort.CreatedBy = HttpContext.User.Identity.Name;
                    cohort = cohortRepository.Add(cohort);

                    return Redirect("/Cohorts/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your response.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "There is a problem saving the cohort.";
            }

            viewModel.CohortTypes = config.Cohorts.ToList().Where(a => a.AllowNew).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(CohortViewModel viewModel)
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");

            try
            {
                if (ModelState.IsValid)
                {

                    var cohortType = config.Cohorts[viewModel.SelectedCohortType];

                    Type t = Type.GetType(cohortType.TypeName);
                    Cohort cohort = Activator.CreateInstance(t) as Cohort;
                    cohort.Name = viewModel.Name;
                    cohort.SystemName = viewModel.SystemName;
                    cohort.TypeName = cohortType.TypeName;
                    cohort.Id = viewModel.Id;

                    viewModel.Properties = new List<CustomCohortPropertyInfo>();

                    foreach (var propInfo in t.GetProperties())
                    {
                        if (Attribute.IsDefined(propInfo, typeof(UserEditableAttribute)))
                        {
                            if (Request["prop-" + propInfo.Name] != null)
                            {
                                if (propInfo.PropertyType == typeof(string))
                                {
                                    propInfo.SetValue(cohort, Request["prop-" + propInfo.Name]);
                                }

                                if (propInfo.PropertyType == typeof(DateTime))
                                {
                                    if (Request["prop-" + propInfo.Name] != string.Empty)
                                    {
                                        propInfo.SetValue(cohort, DateTime.Parse(Request["prop-" + propInfo.Name]));
                                    }
                                }
                               
                            }

                            var name = propInfo.Name;

                            if (Attribute.IsDefined(propInfo, typeof(DisplayAttribute)))
                            {
                                var attr = (DisplayAttribute[])propInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                                if (attr.Length > 0)
                                {
                                    name = attr[0].Name;
                                }
                            }

                            var required = Attribute.IsDefined(propInfo, typeof(RequiredAttribute));

                            viewModel.Properties.Add(new CustomCohortPropertyInfo()
                            {
                                Name = propInfo.Name,
                                DisplayName = name,
                                Required = required,
                                Value = Request["prop-" + propInfo.Name]
                            });
                        }
                    }

                    cohort.UpdatedBy = HttpContext.User.Identity.Name;
                    cohort = cohortRepository.Update(cohort);

                    return Redirect("/Cohorts/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your response.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "There is a problem saving the cohort.";
            }

            viewModel.CohortTypes = config.Cohorts.ToList().Where(a => a.AllowNew).ToList();

            return View(viewModel);
        }

        public ActionResult Questions(string type)
        {
            var config = (MtelligentSection)ConfigurationManager.GetSection("Mtelligent");
            var cohortType = config.Cohorts[type];
            if (cohortType == null)
            {
                return Content("Invalid Cohort Type");
            }

            Type t = Type.GetType(cohortType.TypeName);

            List<CustomCohortPropertyInfo> properties = new List<CustomCohortPropertyInfo>();

            foreach (var propInfo in t.GetProperties())
            {
                if (Attribute.IsDefined(propInfo, typeof(UserEditableAttribute)))
                {
                    var name = propInfo.Name;

                    if (Attribute.IsDefined(propInfo, typeof(DisplayAttribute)))
                    {
                        var attr = (DisplayAttribute[])propInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                        if (attr.Length > 0)
                        {
                            name = attr[0].Name;
                        }
                    }

                    var required = Attribute.IsDefined(propInfo, typeof(RequiredAttribute));

                    properties.Add(new CustomCohortPropertyInfo()
                        {
                         Name = propInfo.Name,
                         DisplayName=name,
                         Required= required
                        });
                }
            }

            return View(properties);
        }
    }
}
