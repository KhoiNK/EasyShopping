using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class ShipperRepository
    {
        EasyShoppingEntities _db;

        const int WAITING_FOR_SHIPPING = 1;

        public ShipperRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool Reject(int id)
        {
            var shipping = _db.ShippingDetails.Where(x => x.ID == id).SingleOrDefault();
            var order = _db.Orders.Include("Store").Where(x => x.ID == shipping.OrderID).SingleOrDefault();
            var shipper = _db.ShipperDetails.Where(x => x.ID == shipping.ShipperID).SingleOrDefault();
            shipper.RecentBalance = shipper.RecentBalance + order.Store.RequiredDeposit;
            order.StatusID = WAITING_FOR_SHIPPING;
            order.IsTaken = false;
            shipping.IsReject = true;
            shipping.ModifiedDate = DateTime.Now;
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<ShipperDetail> GetAll(int pageSize, int pageIndex)
        {
            int skipped = (pageIndex - 1) * pageSize;
            var result = _db.ShipperDetails.Include("User").Where(x => x.StatusId == 1).OrderByDescending(x=>x.RegDate).Skip(skipped).Take(pageSize).ToList();
            return result;
        }

        public ShipperDetail Apply(ShipperDetail data)
        {
            try
            {
                if (IsApplied(data.ShipperId.Value))
                {
                    return null;
                }
                var shipper = new ShipperDetail();
                shipper.BankAccount = data.BankAccount;
                shipper.Deposit = data.Deposit;
                shipper.RecentBalance = data.Deposit;
                shipper.ShipperId = data.ShipperId;
                shipper.StatusId = data.StatusId;
                shipper.RegDate = data.RegDate;
                shipper.Total = 0;
                shipper = _db.ShipperDetails.Add(shipper);
                _db.SaveChanges();
                return shipper;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return null;
            }
        }

        public bool Update(ShipperDetail data)
        {
            try
            {
                var shipper = _db.ShipperDetails.Where(x => x.ID == data.ID).SingleOrDefault();
                shipper.BankAccount = data.BankAccount;
                shipper.Deposit = data.Deposit;
                shipper.StatusId = data.StatusId;
                shipper.RecentBalance = data.RecentBalance;
                shipper.Total = shipper.Total;
                shipper.EndDate = shipper.EndDate;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool Remove(int id)
        {
            var shipper = _db.ShipperDetails.Where(x => x.ID == id).SingleOrDefault();
            _db.ShipperDetails.Remove(shipper);
            return true;
        }

        public ShipperDetail GetById(int id)
        {
            try
            {
                var shipper = _db.ShipperDetails.Where(x => x.ID == id).SingleOrDefault();
                return shipper;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public IEnumerable<ShippingDetail> GetByStore(int storeId)
        {
            try
            {
                var result = _db.ShippingDetails
                    .Include("Order")
                    .Include("ShipperDetail")
                    .Where(x => (x.Order.StoreId == storeId) && (x.Order.StatusID == WAITING_FOR_SHIPPING) && (x.IsReject == false))
                    .ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public bool IsApplied(int userId)
        {
            try
            {
                var result = _db.ShipperDetails.Where(x => (x.ShipperId == userId)).Count();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return false;
            }
        }

        public bool IsShipper(int userId)
        {
            try
            {
                var result = _db.ShipperDetails.Where(x => (x.ShipperId == userId) && (x.StatusId == 2)).Count();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return false;
            }
        }

        public ShipperDetail GetByUserId(int userId)
        {
            try
            {
                var result = _db.ShipperDetails.Where(x => x.ShipperId == userId).Single();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return null;
            }
        }
    }
}
