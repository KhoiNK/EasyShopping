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
        private const int OUTOFSTOCK = 3;
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
            var product = _db.Products.Where(x => x.ID == productId).Single();
            product.Quantity = product.Quantity - 1;
            if (product.Quantity == 0)
            {
                product.StatusID = OUTOFSTOCK;
            }
            _db.SaveChanges();
            return true;
        }

        public bool IsExisted(int productId, int cartId)
        {
            if (_db.OrderDetails.Where(x => (x.OrderID == cartId) && (x.ProductID == productId)).Count() > 0) { return true; }
            return false;
        }

        public bool UpdateQuantity(int productId, int cartId)
        {
            var order = _db.Orders.Where(x => x.ID == cartId).Single();
            var detail = order.OrderDetails.Where(x => x.ProductID == productId).Single();
            detail.Quantity = detail.Quantity + 1;
            var product = _db.Products.Where(x => x.ID == productId).Single();
            product.Quantity = product.Quantity - 1;
            if (product.Quantity == 0)
            {
                product.StatusID = 3;
            }
            _db.SaveChanges();
            return true;
        }

        public bool ChangeQuantity(OrderDetail data)
        {
            try
            {
                var detail = _db.OrderDetails.Where(x => x.ID == data.ID).Single();
                var product = _db.Products.Where(x => x.ID == detail.ProductID).SingleOrDefault();
                if (data.Quantity > detail.Quantity)
                {
                    product.Quantity = product.Quantity - (data.Quantity.Value - detail.Quantity.Value);
                    if (product.Quantity == 0)
                    {
                        product.StatusID = OUTOFSTOCK;
                    }
                    _db.SaveChanges();
                }
                else if (data.Quantity < detail.Quantity)
                {
                    product.Quantity = product.Quantity + (detail.Quantity.Value - data.Quantity.Value);
                    _db.SaveChanges();
                }

                detail.ModifiedID = data.ModifiedID;
                detail.ModifiedDate = data.ModifiedDate;
                detail.Quantity = data.Quantity;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var detail = _db.OrderDetails.Where(x => x.ID == id).Single();
                var product = _db.Products.Where(x => x.ID == detail.ProductID).SingleOrDefault();
                product.Quantity = product.Quantity + detail.Quantity.Value;
                _db.OrderDetails.Remove(detail);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public IEnumerable<OrderDetail> GetByOrderId(int id)
        {
            var details = _db.OrderDetails.Include("Product").Where(x => x.OrderID == id).ToList();
            return details;
        }

        public OrderDetail GetById(int id)
        {
            try
            {
                var detail = _db.OrderDetails.Where(x => x.ID == id).SingleOrDefault();
                return detail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public bool EditDetail(OrderDetail data)
        {
            try
            {
                var result = _db.OrderDetails.Where(x => x.ID == data.ID).SingleOrDefault();
                result.CreatedDate = data.CreatedDate;
                result.ModifiedDate = data.ModifiedDate;
                result.ModifiedID = data.ModifiedID;
                result.OrderID = data.OrderID;
                result.ProductID = data.ProductID;
                result.Quantity = data.Quantity;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public bool IsSameStore(int orderid, int storeId)
        {
            var details = _db.OrderDetails.Where(x => x.OrderID == orderid).ToList();
            foreach (var d in details)
            {
                if(d.Product.StoreID == storeId)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<OrderDetail> GetByStoreId(int storeId, int orderId)
        {
            var result = _db.OrderDetails.Where(x => (x.Product.StoreID == storeId) &&(x.OrderID == orderId)).ToList();
            return result;
        }
    }
}
