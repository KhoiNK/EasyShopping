using System.Web.Mvc;

namespace EasyShopping.Controllers.User
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Login()
        {
            return PartialView();
        }
    }
}