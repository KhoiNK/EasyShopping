using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Mvc.Models;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace EasyShopping.Mvc.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult AddProduct()
        {
            return PartialView();
        }

        public ActionResult ProductDetail()
        {
            return PartialView();
        }

        public ActionResult ApproveList()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }
    }
}