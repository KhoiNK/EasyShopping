using EasyShopping.Api.Models;
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
    public class PartnerController : ApiController
    {
        private PartnerBusiness _business;
        public PartnerController()
        {
            _business = new PartnerBusiness();
        }

        [HttpGet]
        [ActionName("GetList")]
        public IHttpActionResult GetList(int id)
        {
            if(_business.GetList(id).Count() <= 0)
            {
                return BadRequest();
            }
            return Ok(_business.GetList(id));
        }

        [HttpGet]
        [ActionName("Approve")]
        public bool Approve(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return _business.Approve(id, name);
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        [ActionName("Apply")]
        public bool Apply(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return _business.Apply(name, id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
    }
}
