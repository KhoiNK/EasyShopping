using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyShopping.Mvc.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Apply()
        {
            return PartialView();
        }

        public ActionResult GetByStore()
        {
            return PartialView();
        }

        public ActionResult GetApproveList()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return PartialView();
        }
    }
}