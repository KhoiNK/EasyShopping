using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyShopping.Mvc.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult OrderDetail()
        {
            return PartialView();
        }
    }
}