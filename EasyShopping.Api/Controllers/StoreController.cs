﻿using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;


namespace EasyShopping.Api.Controllers
{
    
    public class StoreController : ApiController
    {
        StoreBusinessLogic _business = null;
        public StoreController()
        {
            _business = new StoreBusinessLogic();
        }

        [Authorize]
        public StoreApiModel Post([FromBody]StoreApiModel store)
        {
            try
            {
                //idenity user
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                //identity user/
                store.ModifiedUser = name;
                store.UserName = name;
                var newstore = _business.CreateStore(ApiTranslators.Translate<StoreApiModel, StoreDTO>(store));
                return ApiTranslators.Translate<StoreDTO, StoreApiModel>(newstore);
            }
            catch
            {
                return null;
            }
        }

        [ActionName("Search")]
        [HttpGet]
        public IEnumerable<StoreApiModel> Search(string id)
        {
            return ApiTranslators.Translate<StoreDTO, StoreApiModel>(_business.GetByName(id));
        }


        public StoreApiModel Get(int id)
        {
            return ApiTranslators.Translate<StoreDTO, StoreApiModel>(_business.GetById(id));
        }

        public IEnumerable<StoreApiModel> GetByUserId()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return ApiTranslators.Translate<StoreDTO, StoreApiModel>(_business.GetByUserId(name).Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            
        }

        [Authorize]
        public IHttpActionResult Put([FromBody] StoreApiModel store)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.Put(ApiTranslators.Translate<StoreApiModel, StoreDTO>(store), name);
                if (result) { return Ok(result); }
                else { return BadRequest(); }
            }
            catch
            {
                return BadRequest("false");
            }
        }

        [HttpPut]
        [Authorize]
        [Route("v1/Store/UpgradeStore")]
        public IHttpActionResult UpgradeStore([FromBody] PackageApiModel package)
        {
            var result = _business.UpgradeStore(package.ObjectID, package.PackageID);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public bool Approve(int id)
        {
            return _business.ApproveStore(id);
        }

        [Authorize]
        public bool Delete(int id)
        {
            try {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return _business.Delete(id,name).Result;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        [ActionName("GetList")]
        [Authorize(Roles = Constants.Roles.Admin)]
        public IHttpActionResult GetList(int id)
        {
            var result = _business.GetAll(id, 10);
            return Ok(result);
        }

        #region Allowance
        [HttpGet]
        [ActionName("GetAllowance")]
        [Authorize]
        public bool GetAllowance(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.IsAllowed(name, id);
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        [HttpGet]
        [ActionName("IsOwner")]
        [Authorize]
        public bool IsOwner(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.IsOwner(id, name);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        #endregion
    }
}