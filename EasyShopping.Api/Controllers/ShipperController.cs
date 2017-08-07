using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class ShipperController : ApiController
    {
        private ShipperBusinessLogic _business;
        public ShipperController()
        {
            _business = new ShipperBusinessLogic();
        }

        [HttpGet]
        [ActionName("GetByStore")]
        public IHttpActionResult GetByStore(int id)
        {
            var result = _business.GetByStoreId(id);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("Apply")]
        public IHttpActionResult Apply([FromBody] ShipperDetailApiModel data)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.Apply(ApiTranslators.Translate<ShipperDetailApiModel, ShipperDetailDTO>(data), name);
                return Ok(result);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [ActionName("Approve")]
        [Authorize(Roles = Constants.Roles.Admin)]
        public IHttpActionResult Approve(int id)
        {
            if (_business.Approve(id))
            {
                return Ok(true);
            }
            return BadRequest();
        }

        [HttpGet]
        [ActionName("GetApproveList")]
        [Authorize(Roles = Constants.Roles.Admin)]
        public IHttpActionResult GetApproveList(int id)
        {
            return Ok(_business.GetById(id));
        }

        [HttpDelete]
        [Authorize(Roles = Constants.Roles.Admin)]
        public bool Delete(int id)
        {
            return _business.RemoveShipper(id);
        }

        [HttpGet]
        [ActionName("Reject")]
        public IHttpActionResult Reject(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.Reject(id));
            }
            catch {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("v1/Shipper/IsShipper")]
        public IHttpActionResult IsShipper()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.IsShipper(name));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        [Route("v1/Shipper/IsApplied")]
        public IHttpActionResult IsApplied()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.IsApplied(name));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
