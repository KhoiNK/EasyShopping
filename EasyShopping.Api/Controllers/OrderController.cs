using EasyShopping.Api.Models;
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
    public class OrderController : ApiController
    {
        OrderBusinessLogic _business;
        public OrderController()
        {
            _business = new OrderBusinessLogic();
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody] AddToCartModel data)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                if (data.cartId == 0)
                {
                    var cart = _business.CreateOrder(name, data.productId);
                    if (cart != null) { return Ok(cart); }
                }
                else
                {
                    if (_business.IsExisted(data.productId, data.cartId))
                    {
                        if (_business.AddMoreItem(data.cartId, data.productId)) { return Ok(); }

                    }
                    if (_business.AddToCart(data.productId, data.cartId)) { return Ok(); }
                }
                return BadRequest();
            }
            catch(Exception e)
            {
                
                Console.WriteLine(e.StackTrace);
                return BadRequest();
            }
            
        }

        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            return Ok(ApiTranslators.Translate<OrderDTO, OrderApiModel>(_business.GetByUser(name)));
        }

        public IHttpActionResult Get(int id)
        {
            var order = _business.GetById(id);
            if(order != null)
            {
                return Ok(order);
            }
            return BadRequest();
        }
    }
}
