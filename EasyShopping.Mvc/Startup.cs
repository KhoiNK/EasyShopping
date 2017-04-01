using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyShopping.Mvc.Startup))]
namespace EasyShopping.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
