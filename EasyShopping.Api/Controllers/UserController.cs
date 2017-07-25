using EasyShopping.Api.Constants;
using EasyShopping.Api.Models;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    
    //[Route("v1/nguoidung")]
    [Authorize]
    public class UserController : ApiController
    {
        private UserBusinessLogic _business = null;
        

        public UserController()
        {
            _business = new UserBusinessLogic();
        }

        // GET api/values
        [HttpGet]
        [ActionName("GetUserList")]
        [Authorize(Roles = Roles.Admin)]
        public IEnumerable<UserApiModel> Get()
        {
            //IEnumerable<UserApiModel> userlist = UserTranslator.ToUserApi(_business.GetAll());
            //System.Diagnostics.Debugger.Launch();
            return ApiTranslators.Translate<UserDTO, UserApiModel>(_business.GetAll());
        }

        // GET api/values/5
        [HttpGet]
        [ActionName("GetUserInfo")]
        public UserApiModel GetDetail(int id)
        {
            UserApiModel user = ApiTranslators.Translate<UserDTO, UserApiModel>(_business.GetByID(id));
            return user;
        }

        [HttpGet]
        [ActionName("GetUserName")]
        public async Task<UserApiModel> Get(string username)
        {
            UserDTO user = await _business.GetByName(username);
            return ApiTranslators.Translate<UserDTO, UserApiModel>(user);
        }

        [HttpGet]
        [Route("v1/User/IsAdmin")]
        public bool IsAdmin()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var role = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Single().Value;
                if (role.Equals("Admin"))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        //POST api/values
        public UserApiModel Post([FromBody]AddUserModel user)
        {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {

            }
            UserDTO userdto = ApiTranslators.Translate<AddUserModel, UserDTO>(user);
            UserApiModel newuser = ApiTranslators.Translate<UserDTO, UserApiModel>(_business.Register(userdto).Result); 
            //UserApiModel newuser = ApiTranslators.Translate<UserDTO, UserApiModel>();
            if(newuser != null)
            {
                return newuser;
            }
            return null;
        }

        // PUT api/values/
        //[Authorize(Roles = Roles.Admin)]
        public IHttpActionResult Put([FromBody]AddUserModel value)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            UserDTO user = ApiTranslators.Translate<AddUserModel, UserDTO>(value);
            if (!_business.Update(user))
            {
                return InternalServerError();
            }
            return Ok();
        }

        // DELETE api/values/5
        //[Authorize(Roles = Roles.Admin)]
        public IHttpActionResult Delete(int id)
        {

            if (!_business.Delete(id))
            {
                return InternalServerError();
            }
            return Ok();
        }

    }
}
