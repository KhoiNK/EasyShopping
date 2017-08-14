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
        private const int CANCEL = 5;
        private const int PROCESSING = 6;

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

        public IEnumerable<Order> GetByUserId(int userId, int pageSize, int pageIndex)
        {
            int skipped = (pageIndex - 1) * pageSize;
            var orders = _db.Orders
                .Include("Country")
                .Include("District")
                .Include("Province")
                .Include("OrderStatu")
                .Include("Store")
                .Where(x => (x.UserID == userId) && (x.StatusID != ORDERING)).OrderByDescending(x => x.CreatedDate).ToList().Skip(skipped).Take(pageSize);
            return orders;
        }

        public Order GetById(int id)
        {
            try {
                var order = _db.Orders
                .Include("Country")
                .Include("District")
                .Include("Province")
                .Include("OrderStatu")
                .Include("Store")
                .Include("ShippingDetails")
                .Where(x => x.ID == id).Single();
                return order;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var details = _db.OrderDetails.Where(x => x.OrderID == id).ToList();
                if (details.Count() > 0)
                {
                    foreach (var detail in details)
                    {
                        var product = _db.Products.Where(x => x.ID == detail.ProductID).Single();
                        product.Quantity = product.Quantity + detail.Quantity.Value;
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
                order.CityID = data.CityID;
                order.CountryID = data.CountryID;
                order.Address = data.Address;
                order.IsPaid = data.IsPaid;
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
            
            foreach (var r in result)
            {
                var detail = _db.OrderDetails.Where(x => x.OrderID == r.ID).ToList();
                foreach (var d in detail)
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
            var result = new List<Order>();
            if(id != ORDERING)
            {
                result = _db.Orders.Where(x => (x.StatusID != ORDERING) && (x.UserID == userId)).OrderByDescending(x => x.CreatedDate).ToList();
            }
            else
            {
                result = _db.Orders.Where(x => (x.StatusID == ORDERING) && (x.UserID == userId)).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
            }
            
            return result;
        }

        public IEnumerable<Order> GetByStore(int id)
        {
            try
            {
                var result = _db.Orders
                .Include("Country")
                .Include("District")
                .Include("Province")
                .Include("OrderStatu")
                .Include("ShippingDetails")
                .Include("OrderDetails")
                .Where(x => (x.StoreId == id) && (x.StatusID != ORDERING))
                .OrderBy(x=>x.IsPaid)
                .ToList();
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return null;
            }
        }

        public IEnumerable<Order> GetByParent(int id)
        {
            try {
                var result = _db.Orders.Include("Country")
                .Include("District")
                .Include("Province")
                .Include("OrderStatu")
                .Include("Store")
                .Where(x => x.ParentId == id).ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return null;
            }
        }

        public bool RejectOrder(int id)
        {
            try {
                var details = _db.OrderDetails.Where(x => x.OrderID == id).ToList();
                if(details.Count() > 0)
                {
                    foreach (var p in details)
                    {
                        var product = _db.Products.Where(x => x.ID == p.ProductID).Single();
                        product.Quantity = product.Quantity + p.Quantity.Value;
                        _db.SaveChanges();
                    }
                }
                var order = _db.Orders.Where(x => x.ID == id).Single();
                order.StatusID = CANCEL;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return false;
            }

        }
    }
}
