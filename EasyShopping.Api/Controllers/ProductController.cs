﻿using EasyShopping.Api.Models;
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

        public IHttpActionResult Get()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.GetAllWithUser(name));
            }
            catch
            {
                return Ok(_business.GetAllWithoutUser());
            }
        }
        [HttpPost]
        [ActionName("AddProduct")]
        public IHttpActionResult Post([FromBody] ProductApiModel data)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            ProductDTO newproduct = _business.Add(ApiTranslators.Translate<ProductApiModel, ProductDTO>(data), name);
            ProductApiModel newProduct = ApiTranslators.Translate<ProductDTO, ProductApiModel>(newproduct);
            if (newProduct != null) return Ok(newProduct);
            return BadRequest();
        }

        [HttpGet]
        [ActionName("ApproveList")]
        public IEnumerable<ProductApiModel> GetApproveList(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                if (!_store.IsOwner(id, name))
                {
                    return null;
                }
                var list = ApiTranslators.Translate<ProductDTO, ProductApiModel>(_business.GetApproveList(id));
                return list;
            }
            catch
            {
                return null;
            }
        }

        [HttpPut]
        [ActionName("EditProduct")]
        public IHttpActionResult Put([FromBody] ProductApiModel data)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.Edit(ApiTranslators.Translate<ProductApiModel, ProductDTO>(data), name);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("GetDetail")]
        public ProductApiViewModel GetDetail(int id)
        {
            ProductApiViewModel product = ApiTranslators.Translate<ProductViewDTO, ProductApiViewModel>(_business.GetById(id));
            if (product != null) { return product; }
            return null;
        }

        [HttpGet]
        [ActionName("GetWithName")]
        public IHttpActionResult GetWithName(string id)
        {
            var result = _business.GetByName(id);
            return Ok(result);
        }
        //public 
    }
}