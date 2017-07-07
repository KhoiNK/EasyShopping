using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class DistrictController : ApiController
    {
        private DistrictBusinessLogic _business;
        public DistrictController()
        {
            _business = new DistrictBusinessLogic();
        }

        public IHttpActionResult Get()
        {
            var districts = ApiTranslators.Translate<DistrictDTO, DistrictApiModel>(_business.GetAll());
            if (districts.Count() > 0) { return Ok(districts); }
            return BadRequest();
        }

        public IHttpActionResult Get(int id)
        {
            var district = ApiTranslators.Translate<DistrictDTO, DistrictApiModel>(_business.GetById(id));
            if(district != null) { return Ok(district); }
            return BadRequest();
        }
    }
}
