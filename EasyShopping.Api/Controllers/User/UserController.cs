using EasyShopping.Api.Constants;
using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    [Authorize]
    //[Route("v1/nguoidung")]
    public class UserController : ApiController
    {
        private UserBusinessLogic _business = null;
        

        public UserController()
        {
            _business = new UserBusinessLogic();
        }

        // GET api/values
        public IEnumerable<UserApiModel> Get()
        {
            //IEnumerable<UserApiModel> userlist = UserTranslator.ToUserApi(_business.GetAll());
            //System.Diagnostics.Debugger.Launch();
            return _business.GetAll().Translate<UserDTO, UserApiModel>();
        }

        // GET api/values/5
        public UserApiModel Get(int id)
        {
            UserApiModel user = ApiTranslators.Translate<UserDTO, UserApiModel>(_business.GetByID(id));
            return user;
        }

        public async Task<UserApiModel> Get(string username)
        {
            UserDTO user = await _business.GetByName(username);
            return ApiTranslators.Translate<UserDTO, UserApiModel>(user);
        }

        //POST api/values
        public UserApiModel Post([FromBody]UserApiModel user)
        {
            UserDTO userdto = ApiTranslators.Translate<UserApiModel, UserDTO>(user);
            //UserApiModel newuser = ApiTranslators.Translate<UserDTO, UserApiModel>();
            if(_business.Register(userdto) != null)
            {
                return user;
            }
            return null;
        }

        // PUT api/values/
        [Authorize(Roles = Roles.Admin)]
        public void Put(int id, [FromBody]UserApiModel value)
        {

        }

        // DELETE api/values/5
        [Authorize(Roles = Roles.Admin)]
        public IHttpActionResult Delete(int id)
        {
            if (id % 2 != 0) // Neu ID la so le
            {
                return InternalServerError(new Exception("Minh thich thi minh loi thoi!"));
            }

            if (!_business.Delete(id))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
