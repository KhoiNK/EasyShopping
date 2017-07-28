using EasyShopping.Api.Models;
using EasyShopping.Api.SignalR;
using EasyShopping.BusinessLogic.Business;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace EasyShopping.Api.Controllers
{
    [Authorize]
    public class MessageController : ApiController
    {
        MessageBusinessLogic _business;

        public MessageController()
        {
            _business = new MessageBusinessLogic();
        }

        [HttpPost]
        public MessageApiModel Post(MessageApiModel mess)
        {
            var result = _business.CreateMessage(ApiTranslators.Translate<MessageApiModel, MessageDTO>(mess));
            EasyShoppingHub.PushToUser(result.Sent, result, null);
            return ApiTranslators.Translate<MessageDTO, MessageApiModel>(result);
        }
        
        public IHttpActionResult Get()
        {
            try {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.GetByUserId(name);
                return Ok(result);
            }
            catch
            {
                Console.WriteLine(InternalServerError().ToString());
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("v1/Message/Count")]
        public IHttpActionResult Count()
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                var result = _business.CountUnread(name);
                return Ok(result);
            }
            catch
            {
                Console.WriteLine(InternalServerError().ToString());
                return BadRequest();
            }
        }
        
        [HttpGet]
        [ActionName("MarkAsRead")]
        public IHttpActionResult MarkAsRead(int id)
        {
            return Ok(_business.MarkAsRead(id));
        } 
    }
}
