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
        #region private var
        private OrderRepository _repo;
        private UserRepository _user;
        private OrderDetailRepository _detail;
        private ProductRepository _product;
        private CityRepository _city;
        private CountryRepository _country;
        private DistrictRepository _district;
        private MessageBusinessLogic _message;
        private ShipperRepository _shipper;
        #endregion
        #region CONSTANT
        private const int ORDERING = 4;
        private const int CANCEL = 5;
        private const int PROCESSING = 6;
        private const int TYPE_ORDER = 1;
        private const int WAITINGFORSHIPPING = 1;
        private const int STORE_ORDER = 4;
        private const int PARTNER = 5;
        private const int COMPLETE = 3;
        private const int DELIVERING = 2;
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
            _shipper = new ShipperRepository();
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
            order.details = GetOrderDetail(order.ID);
            order.Children = GetChildrenOrder(id);
            return order;
        }

        public IEnumerable<OrderViewDTO> GetByUser(string username, int pageSize, int pageIndex)
        {
            var userId = _user.FindUser(username).ID;
            var orders = _repo.GetByUserId(userId, pageSize, pageIndex).Translate<Order, OrderViewDTO>();
            foreach (var order in orders)
            {
                if (order.StoreId.HasValue)
                {
                    order.details = _detail.GetByOrderId(order.ID).Translate<OrderDetail, OrderDetailDTO>().ToList();
                }
                else
                {
                    order.details = _repo.GetChildOrderDetail(order.ID).Translate<OrderDetail, OrderDetailDTO>().ToList();

                }
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
            dto.StatusID = PROCESSING;
            dto.Total = 0;
            dto.UserID = userId;
            dto.CityID = _city.GetByName(order.City).Id;
            dto.CountryID = _country.GetByName(order.Country).Id;
            var DisString = order.District.Split('.');
            if (DisString.Length > 1) { dto.DistrictID = _district.GetByName(DisString[1]).Id; }
            else if (DisString.Length == 1) { dto.DistrictID = _district.GetByName(DisString[0]).Id; }
            dto.Address = order.Address;
            if (order.StoreId != null)
            {
                dto.StoreId = order.StoreId.Value;
                dto.Total = dto.Price;
                foreach (var detail in order.details)
                {
                    dto.Total = dto.Total + (detail.Quantity * detail.Price);
                    dto.Price = dto.Price + (detail.Quantity * detail.Weight);
                }
                dto.Price = dto.Price * 5;
                var result = _repo.UpdateOrder(dto.Translate<OrderDTO, Order>());
                if (result)
                {
                    var mess = new MessageDTO();
                    mess.Description = "Order " + order.ID + " is checked out.";
                    mess.FromID = userId;
                    mess.SentID = _repo.GetById(order.ID).Store.UserID;
                    mess.MessageType = STORE_ORDER;
                    mess.DataID = order.StoreId;
                    _message.CreateMessage(mess);
                }
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
                        newOrder.OrderCode = order.OrderCode;
                        newOrder.StatusID = PROCESSING;
                        newOrder.ModifiedDate = DateTime.Now;
                        newOrder.ModifiedID = 1;
                        newOrder.CreatedDate = DateTime.Now;
                        newOrder.UserID = null;
                        newOrder = _repo.Create(newOrder.Translate<OrderDTO, Order>()).Translate<Order, OrderDTO>();
                        newOrder.Address = dto.Address;
                        newOrder.CityID = dto.CityID;
                        newOrder.DistrictID = dto.DistrictID;
                        newOrder.CountryID = dto.CountryID;
                        newOrder.StoreId = d.Value.Product.StoreID;
                        newOrder.ParentId = order.ID;
                        newOrder.Total = newOrder.Price;
                        var childDetails = _detail.GetByStoreId(d.Value.Product.StoreID, order.ID);
                        foreach (var c in childDetails)
                        {
                            var child = _detail.GetById(c.ID);
                            child.ModifiedDate = DateTime.Now;
                            child.OrderID = newOrder.ID;
                            _detail.EditDetail(child);
                            var temp = d.Value;
                            newOrder.Total = newOrder.Total + (c.Quantity.Value * c.Product.Price.Value);
                            newOrder.Price = newOrder.Price + (c.Quantity.Value * c.Product.Weight);
                            details.TryRemove(c.ID, out temp);
                        }
                        newOrder.Price = 5 * newOrder.Price;
                        _repo.UpdateOrder(newOrder.Translate<OrderDTO, Order>());
                        var mess = new MessageDTO();
                        mess.Description = "checked out order " + newOrder.ID;
                        mess.FromID = userId;
                        mess.SentID = _repo.GetById(newOrder.ID).Store.UserID;
                        mess.MessageType = STORE_ORDER;
                        mess.DataID = newOrder.StoreId;
                        _message.CreateMessage(mess);
                        dto.Total = dto.Total + newOrder.Total;
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
                if (fromUserId == toUserID)
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

                mess.FromID = fromUserId;
                mess.SentID = toUserID;
                mess.MessageType = TYPE_ORDER;
                if (parentID.HasValue)
                {
                    mess.Description = "Your order " + parentID.Value + " is canceled.";
                    mess.DataID = parentID.Value;
                }
                else
                {
                    mess.Description = "Your order " + id + " is canceled.";
                    mess.DataID = id;
                }
                _message.CreateMessage(mess);
            }
            return result;
        }

        public bool AcceptOrder(int id, string name)
        {
            var fromUserId = _user.FindUser(name).ID;
            var order = _repo.GetById(id);
            if (order.StatusID == PROCESSING)
            {
                order.StatusID = WAITINGFORSHIPPING;
                order.IsTaken = false;
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
                    if (order.ParentId.HasValue)
                    {
                        mess.Description = "Your order " + order.ParentId.Value + " is Accepted.";
                        mess.DataID = order.ParentId.Value;
                    }
                    else
                    {
                        mess.Description = "Your order " + id + " is Accepted.";
                        mess.DataID = id;
                    }
                    mess.FromID = fromUserId;
                    mess.SentID = toUserID;
                    mess.MessageType = TYPE_ORDER;

                    _message.CreateMessage(mess);
                }
                return result;
            }
            return false;
        }

        public IEnumerable<OrderViewDTO> GetByStore(int storeID)
        {
            var orders = _repo.GetByStore(storeID);
            var result = orders.Translate<Order, OrderViewDTO>();
            var shipdetail = new ShippingDetail();

            foreach (var order in result)
            {
                order.details = orders.Where(x => x.ID == order.ID).Single().OrderDetails.ToList().Translate<OrderDetail, OrderDetailDTO>();

                if (orders.Where(x => x.ID == order.ID).Single().IsTaken == true)
                {
                    var shipper = _shipper.GetByStoreOrder(order.ID);
                    order.Shipper = shipper.User.UserName;
                    order.ShipperID = shipper.User.ID;
                }
            }
            return result;
        }

        public IEnumerable<OrderViewDTO> GetChildrenOrder(int id)
        {
            var result = _repo.GetByParent(id).Translate<Order, OrderViewDTO>();
            foreach (var order in result)
            {
                order.details = GetOrderDetail(order.ID);
            }
            return result;
        }

        public bool Update(OrderViewDTO data)
        {
            var order = _repo.GetById(data.ID);
            order.Address = data.Address;
            order.CreatedDate = data.CreatedDate;
            order.ID = data.ID;
            order.IsPaid = data.IsPaid;
            order.ModifiedDate = data.ModifiedDate;
            order.Note = data.Note;
            order.OrderCode = data.OrderCode;
            order.Price = data.Price;
            order.StatusID = data.StatusID;
            order.StoreId = data.StoreId;
            order.Total = data.Total;
            var result = _repo.UpdateOrder(order);
            return result;
        }
    }
}
