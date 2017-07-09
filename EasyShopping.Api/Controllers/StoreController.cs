using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
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
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            store.ModifiedUser = name;
            store.UserName = name;
            var newstore = _business.CreateStore(ApiTranslators.Translate<StoreApiModel, StoreDTO>(store));
            return store;
        }

        public bool Post(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return _business.IsOwner(id, name);
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public IEnumerable<StoreApiModel> Get(int page, int index = 10)
        {
            return ApiTranslators.Translate<StoreDTO, StoreApiModel>(_business.GetAll(index, page));
        }

        public IEnumerable<StoreApiModel> Get(string searchkey)
        {
            return ApiTranslators.Translate<StoreDTO, StoreApiModel>(_business.GetByName(searchkey));
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

        //public bool Put([FromBody]StoreApiModel store)
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;

        //}

        public IHttpActionResult Put(StoreApiModel store)
        {
            var result = _business.Put(ApiTranslators.Translate<StoreApiModel, StoreDTO>(store));
            if (result) { return Ok(result); }
            else { return BadRequest(); }
        }

        public bool Approve(int id)
        {
            return _business.ApproveStore(id);
        }

        [HttpPost]
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

        [ActionName("IsOwner")]
        public bool IsOwner(int storeId)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.IsOwner(storeId,name);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}