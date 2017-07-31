using EasyShopping.Api.Models;
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public bool Approve(int id)
        {
            return _business.ApproveStore(id);
        }

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

        #region Allowance
        [HttpGet]
        [ActionName("GetAllowance")]
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