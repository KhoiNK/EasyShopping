using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return "Hello Khoi!";
        }
    }
}
