using EasyShopping.BusinessLogic.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class ApprovementController : ApiController
    {
        ProductBusinessLogic _business;
        public ApprovementController()
        {
            _business = new ProductBusinessLogic();
        }

        [ActionName("ProductApprove")]
        public bool Get(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                if (_business.Approve(id, name)) { return true; }
                return false;
            }
            catch
            {
                return false;
            }
            
        }

    }
}
