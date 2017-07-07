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
        private const int WAITTINGFORSHIPPING = 1;
        
        EasyShoppingEntities _db;
        public OrderRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public Order Create(int userId)
        {
            var order = new Order();
            order.UserID = userId;
            order.CreatedDate = DateTime.Now;
            order.ModifiedDate = DateTime.Now;
            order.ModifiedID = userId;
            order.StatusID = WAITTINGFORSHIPPING;
            order = _db.Orders.Add(order);
            _db.SaveChanges();
            return order;
        }

        public IEnumerable<Order> GetByUserId(int userId)
        {
            var orders = _db.Orders.Where(x => x.UserID == userId).ToList();
            return orders;
        }

        public Order GetById(int id)
        {
            var order = _db.Orders.Where(x => x.ID == id).Single();
            return order;
        }
    }
}
