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

        public ProductApiModel Post([FromBody] ProductApiModel data, int storeid)
        {
            ProductDTO newproduct = _business.Add(ApiTranslators.Translate<ProductApiModel, ProductDTO>(data));
            return ApiTranslators.Translate<ProductDTO, ProductApiModel>(newproduct);
        }
    }
}