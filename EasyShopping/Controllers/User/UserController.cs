using System.Threading.Tasks;
using System.Web.Http;

using EasyShopping.Models;
using EasyShopping.BusinessLogic.Business;
//using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Collections;

namespace EasyShopping.Controllers.User
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private UserBusinessLogic _UserBusiness = null;
        public UserController()
        {
            _UserBusiness = new UserBusinessLogic();
        }


        [HttpGet]
        public async Task<IHttpActionResult> Login(string username, string password)
        {
            //var _user = new UserBusinessLogic();
            UserViewModel user = await Task.Run(() =>
            {
                return UserTranslator.ToUserView(_UserBusiness.UserLogin(username, password));
            });

            WaitingMessage("Loging in....");

            if (user == null)
            {
                return NotFound();
            }
            //HttpContext.Current.Session["USER_ID"] = user.ID;
            return Ok(user);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Register([FromBody] UserViewModel userview)
        {
            UserViewModel user = await Task.Run(() =>
            {
                return UserTranslator.ToUserView(_UserBusiness.Register(UserTranslator.ToUserBusiness(userview)));
            });
            WaitingMessage("Waiting for loging in.....");
            if(user == null)
            {
                return BadRequest("Register failed!");
            }

            var password = userview.PassWord;
            Login(user.UserName, password).RunSynchronously();
            return Ok(user);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] string id)
        {
            if (_UserBusiness.DeleteUser(int.Parse(id.Trim())))
            {
                return Ok("Remove Successfully!");
            }
            return BadRequest("Remove failed!");
        }

        public IHttpActionResult WaitingMessage(string mess)
        {
            return Ok(mess);
        }

        public IEnumerable<UserViewModel> GetAllUser()
        {
            var userlist = UserTranslator.ToUserView(_UserBusiness.GetAllUser());
            return userlist;
        }
    }
}