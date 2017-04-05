using System.Web.Mvc;

namespace EasyShopping.Controllers.User
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}