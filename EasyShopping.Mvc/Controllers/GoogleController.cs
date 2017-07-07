using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyShopping.Mvc.Controllers
{
    public class GoogleController : Controller
    {
        // GET: Google
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}