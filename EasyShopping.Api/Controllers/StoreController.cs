using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<StoreApiModel> Get(int page, int index = 10)
        {
            return _business.GetAll(index, page).Translate<StoreDTO, StoreApiModel>();
        }

        public IEnumerable<StoreApiModel> Get(string searchkey)
        {
            return _business.GetByName(searchkey).Translate<StoreDTO, StoreApiModel>();
        }

        public StoreApiModel Get(int id)
        {
            return ApiTranslators.Translate<StoreDTO, StoreApiModel>(_business.GetById(id));
        }

        public IEnumerable<StoreApiModel> GetByUserId(int id)
        {
            return _business.GetByUserId(id).Result.Translate<StoreDTO, StoreApiModel>();
        }

        //public bool Put([FromBody]StoreApiModel store)
        //{
        //    var identity = (ClaimsIdentity)User.Identity;
        //    var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;

        //}
        
        public bool Put(int id)
        {
            return _business.ApproveStore(id);
        }
    }
}