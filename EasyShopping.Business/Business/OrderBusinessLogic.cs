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
        private CityRepository _city;
        private CountryRepository _country;
        private DistrictRepository _district;

        public OrderBusinessLogic()
        {
            _repo = new OrderRepository();
            _user = new UserRepository();
            _detail = new OrderDetailRepository();
            _product = new ProductRepository();
            _country = new CountryRepository();
            _city = new CityRepository();
            _district = new DistrictRepository();
        }

        public OrderViewDTO CreateOrder(string username, int productId)
        {
            int userId = _user.FindUser(username).ID;
            var storeId = _product.GetById(productId).StoreID;
            var cart = _repo.Create(userId, CodeGenerator.RandomString(6), storeId).Translate<Order, OrderViewDTO>();
            _detail.AddItem(productId, cart.ID);
            return cart;
        }

        public IEnumerable<OrderDetailDTO> GetOrderDetail(int id)
        {
            var result = _detail.GetByOrderId(id).Translate<OrderDetail, OrderDetailDTO>();
            return result;
        }

        public bool AddToCart(int producId, int cartId)
        {
            return _detail.AddItem(producId, cartId);
        }

        public OrderViewDTO GetById(int id)
        {
            var order = _repo.GetById(id).Translate<Order, OrderViewDTO>();
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

        public IEnumerable<OrderViewDTO> GetByUser(string username)
        {
            var userId = _user.FindUser(username).ID;
            return _repo.GetByUserId(userId).Translate<Order, OrderViewDTO>();
        }

        public bool AddMoreItem(int cartId, int productId)
        {
            return _detail.UpdateQuantity(productId, cartId);
        }

        public bool ChangeQuantity(OrderDetailDTO data, string username)
        {
            var detail = new OrderDetail();
            detail.ModifiedID = _user.FindUser(username).ID;
            detail.ModifiedDate = DateTime.Now;
            detail.ID = data.ID;
            detail.Quantity = data.Quantity;
            var result = _detail.ChangeQuantity(detail);
            return result;
        }

        public bool RemoveItem(int id)
        {
            var result = _detail.Remove(id);
            return result;
        }

        public bool RemoveOrder(int id)
        {
            var result = _repo.Remove(id);
            return false;
        }

        public bool IsExisted(int productId, int cartId)
        {
            return _detail.IsExisted(productId, cartId);
        }

        public bool CheckOut(OrderViewDTO order, string username)
        {
            var dto = new OrderDTO();
            dto.ID = order.ID;
            dto.ModifiedDate = DateTime.Now;
            dto.ModifiedID = _user.FindUser(username).ID;
            dto.Note = order.Note;
            dto.OrderCode = order.OrderCode;
            dto.StatusID = order.StatusID;
            dto.StoreId = order.StoreId;
            dto.Total = order.Total;
            dto.UserID = _user.FindUser(username).ID;
            dto.Price = order.Price;
            dto.CityID = _city.GetByName(order.City).Id;
            dto.CountryID = _country.GetByName(order.Country).Id;
            dto.DistrictID = _district.GetByName(order.District).Id;
            var result = _repo.CheckOut(dto.Translate<OrderDTO, Order>());
            return result;
        }
    }
}
