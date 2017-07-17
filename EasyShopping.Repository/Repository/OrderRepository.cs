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
        public OrderRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public Order Create(int userId, string orderCode, int storeId)
        {
            var order = new Order();
            order.UserID = userId;
            order.CreatedDate = DateTime.Now;
            order.ModifiedDate = DateTime.Now;
            order.ModifiedID = userId;
            order.StatusID = ORDERING;
            order.OrderCode = orderCode;
            order.StoreId = storeId;
            order = _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public bool CheckOut(Order data)
        {
            //Target(data);
            try
            {
                var order = _db.Orders.Where(x => x.ID == data.ID).Single();
                order.StatusID = WAITINGFORSHIPPING;
                order.Address = data.Address;
                order.CityID = data.CityID;
                order.CountryID = data.CountryID;
                order.DistrictID = data.DistrictID;
                order.Note = order.Note;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
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
                var order = _db.Orders.Where(x => x.ID == id).Single();
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
    }
}
