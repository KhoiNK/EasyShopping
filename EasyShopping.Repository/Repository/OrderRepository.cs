using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyShopping.Repository.Repository
{
    public class OrderRepository
    {
        private const int WAITINGFORSHIPPING = 1;
        private const int ORDERING = 4;

        EasyShoppingEntities _db;
        OrderDetailRepository _detail;
        public OrderRepository()
        {
            _db = new EasyShoppingEntities();
            _detail = new OrderDetailRepository();
        }

        public Order Create(Order order)
        {
            try
            {
                var neworder = new Order();
                neworder.OrderCode = order.OrderCode;
                neworder.ModifiedDate = order.ModifiedDate;
                neworder.CreatedDate = order.CreatedDate;
                neworder.StatusID = order.StatusID;
                neworder.UserID = order.UserID;
                neworder.StoreId = order.StoreId;
                neworder.ModifiedID = order.ModifiedID;
                neworder = _db.Orders.Add(neworder);
                _db.SaveChanges();
                return neworder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //public bool CheckOut(Order data)
        //{
        //    Target(data);
        //    try
        //    {
        //        //var test = _db.OrderDetails.Where(x => x.OrderID == data.ID).Select(x => x.Product.StoreID).ToList();

        //        var order = _db.Orders.Where(x => x.ID == data.ID).Single();
        //        order.StatusID = WAITINGFORSHIPPING;
        //        order.Address = data.Address;
        //        order.CityID = data.CityID;
        //        order.CountryID = data.CountryID;
        //        order.DistrictID = data.DistrictID;
        //        order.Note = order.Note;
        //        _db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.InnerException);
        //        return false;
        //    }
        //}

        public void Target(Order data)
        {
            var details = _db.OrderDetails.Where(x => x.OrderID == data.ID);
            foreach (var detail in details)
            {
                try
                {
                    var target = _db.Targets.Where(x => (x.UserId == data.UserID) && (x.ProductTypeId == detail.Product.ProductTypeID)).SingleOrDefault();

                    target.Count = target.Count++;
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    var newTarget = new Target();
                    newTarget.UserId = data.UserID;
                    newTarget.ProductTypeId = detail.Product.ProductTypeID;
                    newTarget.Count = 1;
                    _db.Targets.Add(newTarget);
                    _db.SaveChanges();
                }
            }
        }

        public IEnumerable<Order> GetByUserId(int userId)
        {
            var orders = _db.Orders
                .Include("Country")
                .Include("District")
                .Include("Province")
                .Include("OrderStatu")
                .Where(x => x.UserID == userId).ToList();
            return orders;
        }

        public Order GetById(int id)
        {
            var order = _db.Orders
                .Include("Country")
                .Include("District")
                .Include("Province")
                .Include("OrderStatu")
                .Where(x => x.ID == id).Single();
            return order;
        }

        public bool Remove(int id)
        {
            try
            {
                var details = _db.OrderDetails.Where(x => x.OrderID == id).ToList();
                if(details.Count() > 0)
                {
                    foreach (var detail in details)
                    {
                        _db.OrderDetails.Remove(detail);
                    }
                }
                var order = _db.Orders.Where(x => x.ID == id).Single();
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public bool UpdateOrder(Order data)
        {
            try
            {
                var order = _db.Orders.Where(x => x.ID == data.ID).SingleOrDefault();
                order.ModifiedDate = data.ModifiedDate;
                order.ModifiedID = data.ModifiedID;
                order.Note = data.Note;
                order.OrderCode = data.OrderCode;
                order.ParentId = data.ParentId;
                order.Price = data.Price;
                order.StatusID = data.StatusID;
                order.StoreId = data.StoreId;
                order.Total = data.Total;
                if (data.UserID.HasValue)
                {
                    order.UserID = data.UserID.Value;
                }
                if (data.DistrictID != 0 && data.DistrictID != null)
                {
                    order.DistrictID = data.DistrictID;
                }
                else
                {
                    order.DistrictID = null;
                }
                order.CountryID = data.CountryID;
                order.CityID = data.CityID;
                order.Address = data.Address;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.InnerException.StackTrace);
                Console.WriteLine(e.InnerException.InnerException.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public IEnumerable<OrderDetail> GetChildOrderDetail(int parentId)
        {
            var result = _db.Orders.Where(x => x.ParentId == parentId).ToList();
            var details = new List<OrderDetail>();
            foreach(var r in result)
            {
                var detail = _db.OrderDetails.Where(x => x.OrderID == r.ID).ToList();
                foreach(var d in detail)
                {
                    details.Add(d);
                }
            }
            return details;
        }

        public int HasParent(int id)
        {
            var count = _db.Orders.Where(x => x.ParentId == id).Count();
            return count;
        }

        public IEnumerable<Order> GetByStatus(int id, int userId)
        {
            var result = _db.Orders.Where(x => (x.StatusID == id) && (x.UserID == userId)).OrderByDescending(x=>x.CreatedDate).Take(5).ToList();
            return result;
        }
    }
}
