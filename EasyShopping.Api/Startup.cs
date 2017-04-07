using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using EasyShopping.BusinessLogic.Models;

[assembly: OwinStartup(typeof(EasyShopping.Api.Startup))]

namespace EasyShopping.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);
            app.UseWebApi(config);

            BusinessTranslators.Init();
        }
    }
}
