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
    public class CityController : ApiController
    {
        CityBusinessLogic _business;

        public CityController()
        {
            _business = new CityBusinessLogic();
        }

        public IHttpActionResult Get()
        {
            IEnumerable<ProvinceApiModel> provinces = ApiTranslators.Translate<ProvinceDTO, ProvinceApiModel>(_business.GetAll());
            if (provinces.Count() > 0)
            {
                return Ok(provinces);
            }
            return BadRequest();
        }

        public IHttpActionResult Get(int id)
        {
            ProvinceApiModel province = ApiTranslators.Translate<ProvinceDTO, ProvinceApiModel>(_business.GetById(id));
            if(province != null) { return Ok(province); }
            return BadRequest();
        }
    }
}
