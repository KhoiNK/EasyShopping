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
    //[Authorize]
    //[Route("v1/nguoidung")]
    public class UserController : ApiController
    {
        //private UserBusinessLogic _business = null;

        // GET api/values
        public IEnumerable<UserApiModel> Get()
        {
            UserBusinessLogic _business = new UserBusinessLogic();
            //IEnumerable<UserApiModel> userlist = UserTranslator.ToUserApi(_business.GetAll());
            return _business.GetAll().Translate<UserDTO, UserApiModel>();
        }

        // GET api/values/5
        public UserApiModel Get(int id)
        {
            UserBusinessLogic _business = new UserBusinessLogic();
            UserApiModel user = ApiTranslators.Translate<UserDTO, UserApiModel>(_business.GetUserByID(id));
            return user;
        }

        //POST api/values
        public UserApiModel Post([FromBody]UserApiModel user)
        {
            UserBusinessLogic _business = new UserBusinessLogic();
            if(_business.Register(user.Translate<UserApiModel,UserDTO>()) == null)
            {
                return null;
            }
            return user;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]UserApiModel value)
        {

        }

        // DELETE api/values/5
        public bool Delete(int id)
        {
            UserBusinessLogic _business = new UserBusinessLogic();
            if (!_business.Delete(id))
            {
                return false;
            }
            return true;
        }
    }
}
