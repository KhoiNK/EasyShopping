using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.CommonMethod;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Concurrent;
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
        private MessageBusinessLogic _message;
        private const int ORDERING = 4;
        private const int CANCEL = 5;
        private const int PROCESSING = 6;
        private const int TYPE_ORDER = 1;

        #region Temp Data
        private const int WAITINGFORSHIPPING = 1;
        private const string STORAGE_ADDRESS = "386 Nui Thanh, Hai Chau, Da Nang";
        private const int CITY_ID = 48;
        private const int DISTRICT_ID = 492;
        private const int COUNTRY_ID = 237;
        #endregion

        public OrderBusinessLogic()
        {
            _repo = new OrderRepository();
            _user = new UserRepository();
            _detail = new OrderDetailRepository();
            _product = new ProductRepository();
            _country = new CountryRepository();
            _city = new CityRepository();
            _district = new DistrictRepository();
            _message = new MessageBusinessLogic();
        }

        public OrderViewDTO CreateOrder(string username, int productId)
        {
            var order = new OrderDTO();
            int userId = _user.FindUser(username).ID;
            order.UserID = userId;
            order.StoreId = _product.GetById(productId).StoreID;
            order.OrderCode = CodeGenerator.RandomString(6);
            order.CreatedDate = DateTime.Now;
            order.ModifiedDate = DateTime.Now;
            order.StatusID = ORDERING;
            order.ModifiedID = userId;
            var cart = _repo.Create(order.Translate<OrderDTO, Order>()).Translate<Order, OrderViewDTO>();
            _detail.AddItem(productId, cart.ID);
            return cart;
        }

        public IEnumerable<OrderDetailDTO> GetOrderDetail(int id)
        {
            var result = new List<OrderDetailDTO>();
            if (_repo.HasParent(id) == 0)
            {
                result = _detail.GetByOrderId(id).Translate<OrderDetail, OrderDetailDTO>().ToList();
            }
            else
            {
                result = _repo.GetChildOrderDetail(id).Translate<OrderDetail, OrderDetailDTO>().ToList();
            }
            return result;
        }

        public bool AddToCart(int producId, int cartId)
        {
            var cart = _repo.GetById(cartId);
            if (!_detail.IsSameStore(cart.ID, _product.GetById(producId).StoreID))
            {
                cart.StoreId = null;
                _repo.UpdateOrder(cart);
            }
            return _detail.AddItem(producId, cartId);
        }

        public OrderViewDTO GetById(int id)
        {
            var context = _repo.GetById(id);
            var order = context.Translate<Order, OrderViewDTO>();
            
            return order;
        }

        public IEnumerable<OrderViewDTO> GetByUser(string username)
        {
            var userId = _user.FindUser(username).ID;
            var orders = _repo.GetByUserId(userId).Translate<Order, OrderViewDTO>();
            foreach (var order in orders)
            {
                order.details = GetOrderDetail(order.ID);
            }
            return orders;
        }

        public bool AddMoreItem(int cartId, int productId)
        {
            return _detail.UpdateQuantity(productId, cartId);
        }

        public bool ChangeQuantity(OrderDetailDTO data, string username)
        {
            var oldDetail = _detail.GetById(data.ID);
            if (_product.GetById(oldDetail.ProductID.Value).Quantity < (data.Quantity - oldDetail.Quantity))
            {
                return false;
            }
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
            return result;
        }

        public bool IsExisted(int productId, int cartId)
        {
            return _detail.IsExisted(productId, cartId);
        }

        public bool CheckOut(OrderViewDTO order, string username)
        {
            var dto = new OrderDTO();
            int userId = _user.FindUser(username).ID;
            dto.ID = order.ID;
            dto.ModifiedDate = DateTime.Now;
            dto.ModifiedID = userId;
            dto.Note = order.Note;
            dto.OrderCode = order.OrderCode;
            dto.StatusID = WAITINGFORSHIPPING;
            dto.Total = order.Total;
            dto.UserID = userId;
            dto.Price = 30000;
            dto.CityID = _city.GetByName(order.City).Id;
            dto.CountryID = _country.GetByName(order.Country).Id;
            var DisString = order.District.Split('.');
            if (DisString.Length > 1) { dto.DistrictID = _district.GetByName(DisString[1]).Id; }
            else if (DisString.Length == 1) { dto.DistrictID = _district.GetByName(DisString[0]).Id; }
            dto.Address = order.Address;
            if (order.StoreId != null)
            {
                dto.StoreId = order.StoreId.Value;
                var result = _repo.UpdateOrder(dto.Translate<OrderDTO, Order>());
                return result;
            }
            else
            {
                var details = new ConcurrentDictionary<int, OrderDetail>();
                foreach (var d in _detail.GetByOrderId(order.ID).ToList())
                {
                    details.TryAdd(d.ID, d);
                }
                try
                {
                    foreach (var d in details)
                    {
                        var newOrder = new OrderDTO();
                        newOrder.OrderCode = CodeGenerator.RandomString(6);
                        newOrder.StatusID = WAITINGFORSHIPPING;
                        newOrder.ModifiedDate = DateTime.Now;
                        newOrder.ModifiedID = 1;
                        newOrder.CreatedDate = DateTime.Now;
                        newOrder.UserID = null;
                        newOrder = _repo.Create(newOrder.Translate<OrderDTO, Order>()).Translate<Order, OrderDTO>();
                        newOrder.Address = STORAGE_ADDRESS;
                        newOrder.CityID = CITY_ID;
                        newOrder.DistrictID = DISTRICT_ID;
                        newOrder.CountryID = COUNTRY_ID;
                        newOrder.StoreId = d.Value.Product.StoreID;
                        newOrder.ParentId = order.ID;
                        newOrder.Price = 30000;
                        _repo.UpdateOrder(newOrder.Translate<OrderDTO, Order>());
                        var childDetails = _detail.GetByStoreId(d.Value.Product.StoreID, order.ID);
                        foreach (var c in childDetails)
                        {
                            var child = _detail.GetById(c.ID);
                            child.ModifiedDate = DateTime.Now;
                            child.OrderID = newOrder.ID;
                            _detail.EditDetail(child);
                            var temp = d.Value;
                            details.TryRemove(c.ID, out temp);
                        }
                    }
                    dto.StoreId = null;
                    dto.ParentId = null;
                    var result = _repo.UpdateOrder(dto.Translate<OrderDTO, Order>());
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    return false;
                }
            }
        }

        public IEnumerable<OrderViewDTO> GetByStatus(int id, string username)
        {
            var userId = _user.FindUser(username).ID;
            var orders = _repo.GetByStatus(id, userId).Translate<Order, OrderViewDTO>();
            foreach (var order in orders)
            {
                order.details = GetOrderDetail(order.ID);
            }
            return orders;
        }

        public bool CancelOrder(int id, string name)
        {
            var fromUserId = _user.FindUser(name).ID;
            int? parentID = _repo.GetById(id).ParentId;
            int toUserID = 0;
            if (parentID.HasValue)
            {
                toUserID = _repo.GetById(parentID.Value).UserID.Value;
                if(fromUserId == toUserID)
                {
                    toUserID = _repo.GetById(id).Store.UserID;
                }
            }
            else
            {
                toUserID = _repo.GetById(id).UserID.Value;
                if (fromUserId == toUserID)
                {
                    toUserID = _repo.GetById(id).Store.UserID;
                }
            }
            var result = _repo.RejectOrder(id);
            if (result)
            {
                var mess = new MessageDTO();
                mess.Description = "Your order " + id + " is canceled.";
                mess.FromID = fromUserId;
                mess.SentID = toUserID;
                mess.MessageType = TYPE_ORDER;
                mess.DataID = id;
                _message.CreateMessage(mess);
            }
            return result;
        }

        public bool AcceptOrder(int id, string name)
        {
            var fromUserId = _user.FindUser(name).ID;
            var order = _repo.GetById(id);
            order.StatusID = PROCESSING;
            int toUserID = 0;
            if (order.ParentId.HasValue)
            {
                toUserID = _repo.GetById(order.ParentId.Value).UserID.Value;
            }
            else
            {
                toUserID = order.UserID.Value;
            }
            var result = _repo.UpdateOrder(order);
            if (result)
            {
                var mess = new MessageDTO();
                mess.Description = "Your order " + id + " is canceled.";
                mess.FromID = fromUserId;
                mess.SentID = toUserID;
                mess.MessageType = TYPE_ORDER;
                mess.DataID = id;
                _message.CreateMessage(mess);
            }
            return result;
        }
    }
}
