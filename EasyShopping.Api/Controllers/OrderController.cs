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
        ProductBusinessLogic _product;
        public OrderController()
        {
            _business = new OrderBusinessLogic();
            _product = new ProductBusinessLogic();
        }
        [HttpPost]
        [ActionName("Post")]
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
                    bool result = false;
                    if (_business.IsExisted(data.productId, data.cartId))
                    {
                        result = _business.AddMoreItem(data.cartId, data.productId);
                        if (result) { return Ok(result); }
                    }
                    else
                    {
                        result = _business.AddToCart(data.productId, data.cartId);
                        return Ok(result);
                    }
                }
                return BadRequest();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
                return BadRequest();
            }

        }

        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
            return Ok(_business.GetByUser(name));
        }

        [HttpGet]
        [ActionName("GetDetail")]
        public IHttpActionResult Get(int id)
        {
            var order = _business.GetById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return BadRequest();
        }

        [HttpGet]
        [ActionName("GetOrderDetail")]
        public IHttpActionResult GetOrderDetail(int id)
        {
            var result = _business.GetOrderDetail(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("v1/Order/CheckOut")]
        public IHttpActionResult CheckOut([FromBody] OrderViewDTO order)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.CheckOut(order, name));
            }
            catch
            {
                return Ok(false);
            }
        }

        [HttpPut]
        [Route("v1/Order/ChangeQuantity")]
        public bool ChangeQuantity([FromBody] OrderDetailDTO data)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                if (data.Quantity > _product.GetQuantity(data.ProductID))
                {
                    return false;
                }
                return _business.ChangeQuantity(data, name);
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [ActionName("RemoveOrder")]
        [Authorize]
        public bool RemoveOrder(int id)
        {
            var result = _business.RemoveOrder(id);
            return result;
        }

        [HttpDelete]
        [ActionName("RemoveItem")]
        [Authorize]
        public bool RemoveItem(int id)
        {
            var result = _business.RemoveItem(id);
            return result;
        }

        [HttpGet]
        [ActionName("GetByStatus")]
        public IHttpActionResult GetByStatus(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.GetByStatus(id, name));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("CancelOrder")]
        public IHttpActionResult CancelOrder(int id)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                var name = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Single().Value;
                return Ok(_business.CancelOrder(id, name));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
