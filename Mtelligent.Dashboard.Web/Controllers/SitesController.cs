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
    public class SitesController : Controller
    {
        private ISiteRepository siteRepository;

        public SitesController()
        {
            this.siteRepository = new SiteRepository();
        }

        //
        // GET: /Sites/

        public ActionResult Index()
        {
            var sites = siteRepository.GetAll();
            return View(sites);
        }


        public ActionResult Add()
        {
            var site = new Site();
            return View(site);
        }

        [HttpPost]
        public ActionResult Add(Site site)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    site.CreatedBy = HttpContext.User.Identity.Name;
                    site = siteRepository.Add(site);
                    return Redirect("/Sites/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your response.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that site.";
            }

            return View(site);
        }


        public ActionResult Update(int Id)
        {
            var site = siteRepository.Get(Id);
            return View(site);
        }

        [HttpPost]
        public ActionResult Update(Site site)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    site.UpdatedBy = HttpContext.User.Identity.Name;
                    site = siteRepository.Update(site);
                    return Redirect("/Sites/");
                }
                else
                {
                    ViewBag.ErrorMessage = "There is a problem with one of your response.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An Error Occurred while attempting to save that site.";
            }

            return View(site);
        }

        [HttpPost]
        public ActionResult AddUrl(SiteUrl item)
        {
            try
            {
                item.CreatedBy = HttpContext.User.Identity.Name;
                var returnUrl = siteRepository.AddSiteUrl(item);
                return Json(returnUrl);
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }

        [HttpPost]
        public ActionResult DeleteUrl(SiteUrl item)
        {
            try
            {
                item.UpdatedBy = HttpContext.User.Identity.Name;
                siteRepository.DeleteSiteUrl(item);
                return Json("success");
            }
            catch (Exception ex)
            {
                return Json("failure");
            }
        }
    }


}
