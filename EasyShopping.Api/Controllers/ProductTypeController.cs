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
    public class ProductTypeController : ApiController
    {
        ProductTypeBusiness _business = null;

        public ProductTypeController()
        {
            _business = new ProductTypeBusiness();
        }

        public IEnumerable<ProductTypeApiModel> Get()
        {
            return ApiTranslators.Translate<ProductTypeDTO, ProductTypeApiModel>(_business.GetAll());
        }

        [HttpGet]
        [Route("v1/ProductType/GetWithTarget")]
        public IHttpActionResult GetWithTarget()
        {
            return Ok(_business.GetWithTarget());
        }
    }
}
