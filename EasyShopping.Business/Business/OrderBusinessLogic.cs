using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.CommonMethod;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.BusinessLogic.Business
{
    public class OrderBusinessLogic
    {
        OrderRepository _repo;
        UserRepository _user;
        OrderDetailRepository _detail;
        ProductRepository _product;

        public OrderBusinessLogic()
        {
            _repo = new OrderRepository();
            _user = new UserRepository();
            _detail = new OrderDetailRepository();
            _product = new ProductRepository();
        }

        public OrderDTO CreateOrder(string username, int productId)
        {
            int userId = _user.FindUser(username).ID;
            var storeId = _product.GetById(productId).StoreID;
            var cart = _repo.Create(userId, CodeGenerator.RandomString(6), storeId).Translate<Order, OrderDTO>();
            _detail.AddItem(productId, cart.ID);
            return cart;
        }

        public bool AddToCart(int producId, int cartId)
        {
            return _detail.AddItem(producId, cartId);
        }

        public OrderDTO GetById(int id)
        {
            var order = _repo.GetById(id).Translate<Order, OrderDTO>();
            var products = new List<ProductDTO>();
            foreach(var result in _detail.GetByOrderId(order.ID))
            {
                var product = _product.GetById(result.ProductID.Value);
                product.Quantity = result.Quantity.Value;
                products.Add(product.Translate<Product, ProductDTO>());
            }
            order.Products = products;
            return order;
        }

        public IEnumerable<OrderDTO> GetByUser(string username)
        {
            var userId = _user.FindUser(username).ID;
            return _repo.GetByUserId(userId).Translate<Order, OrderDTO>();
        }

        public bool AddMoreItem(int cartId, int productId)
        {
            return _detail.UpdateQuantity(productId, cartId);
        }

        public bool IsExisted(int productId, int cartId)
        {
            return _detail.IsExisted(productId, cartId);
        }
    }
}
