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
        StoreBusinessLogic _store;
        public ProductController()
        {
            _business = new ProductBusinessLogic();
            _store = new StoreBusinessLogic();
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

        public IHttpActionResult Approve(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            if (_business.Approve(id, name)) { return Ok(); }
            return BadRequest();
        }

        public IHttpActionResult GetApproveList()
        {
            return Ok(ApiTranslators.Translate<ProductDTO, ProductApiModel>(_business.GetApproveList()));
        }

        public IHttpActionResult Put([FromBody] ProductApiModel data)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            var result = _business.Edit(ApiTranslators.Translate<ProductApiModel, ProductDTO>(data), name);
            if (result) { return Ok(); }
            return BadRequest();
        }

        public IHttpActionResult Get(int id)
        {
            ProductApiViewModel product = ApiTranslators.Translate<ProductViewDTO, ProductApiViewModel>(_business.GetById(id));
            if(product != null) { return Ok(product); }
            return BadRequest();
        }
        //public 
    }
}