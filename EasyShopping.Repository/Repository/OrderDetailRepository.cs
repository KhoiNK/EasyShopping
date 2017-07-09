using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class OrderDetailRepository
    {
        EasyShoppingEntities _db;
        public OrderDetailRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool AddItem(int productId, int cartId)
        {
            var detail = new OrderDetail();
            detail.OrderID = cartId;
            detail.ProductID = productId;
            detail.Quantity = 1;
            detail.CreatedDate = DateTime.Now;
            detail.ModifiedDate = DateTime.Now;
            _db.OrderDetails.Add(detail);
            _db.Products.Where(x => x.ID == productId).Single().Quantity = _db.Products.Where(x => x.ID == productId).Single().Quantity - 1;
            _db.SaveChanges();
            return true;               
        }

        public bool IsExisted(int productId, int cartId)
        {
            if(_db.OrderDetails.Where(x=>(x.OrderID == cartId) && (x.ProductID == productId)).Count() > 0) { return true; }
            return false;
        }

        public bool UpdateQuantity(int productId, int cartId)
        {
            var order = _db.Orders.Where(x => x.ID == cartId).Single();
            var detail = order.OrderDetails.Where(x => x.ProductID == productId).Single();
            detail.Quantity = detail.Quantity + 1;
            _db.Products.Where(x => x.ID == productId).Single().Quantity = _db.Products.Where(x => x.ID == productId).Single().Quantity - 1;
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<OrderDetail> GetByOrderId(int id)
        {
            var details = _db.OrderDetails.Where(x => x.OrderID == id).ToList();
            return details;
        }
    }
}
