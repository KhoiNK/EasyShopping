using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyShopping.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            return PartialView("~/Views/Shared/Header.cshtml");
        }

        public ActionResult Footer()
        {
            return PartialView("~/Views/Shared/Footer.cshtml");
        }

        public ActionResult SideBar()
        {
            return PartialView("~/Views/Shared/SideBar.cshtml");
        }

        public ActionResult Slider()
        {
            return PartialView("~/Views/Shared/Slider.cshtml");
        }

        public ActionResult SearchPage()
        {
            return PartialView("~/Views/Shared/SearchPage.cshtml");
        }
    }
}