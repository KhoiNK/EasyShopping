using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyShopping.Mvc.Controllers
{
    public class StoreController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult AddStore()
        {
            return PartialView();
        }

        public ActionResult EditStore()
        {
            return PartialView();
        }

        public ActionResult StoreDetail()
        {
            return PartialView();
        }

        public ActionResult StoreListByUser()
        {
            return PartialView();
        }

        public ActionResult ApproveList()
        {
            return PartialView();
        }

        public ActionResult Introduction()
        {
            return PartialView();
        }

        public ActionResult UpgradeStore()
        {
            return PartialView();
        }
    }
}