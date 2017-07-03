using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    public class ProductController : ApiController
    {
        ProductBusinessLogic _business;
        public ProductController()
        {
            _business = new ProductBusinessLogic();
        }

        //public Task<IEnumerable<ProductApiModel>> Get()
        //{

        //}

        public IHttpActionResult Post([FromBody] ProductApiModel data)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            ProductDTO newproduct = _business.Add(ApiTranslators.Translate<ProductApiModel, ProductDTO>(data), name);
            ProductApiModel newProduct = ApiTranslators.Translate<ProductDTO, ProductApiModel>(newproduct);
            if (newProduct != null) return Ok(newProduct);
            return BadRequest();
        }
    }
}