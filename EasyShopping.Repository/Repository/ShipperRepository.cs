﻿using EasyShopping.Repository.Models.Entity;
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
            var shipper = _db.ShippingDetails.Where(x => x.ID == id).SingleOrDefault();
            var order = _db.Orders.Where(x => x.ID == shipper.OrderID).SingleOrDefault();
            order.StatusID = WAITING_FOR_SHIPPING;
            order.IsTaken = false;
            shipper.IsReject = true;
            _db.SaveChanges();
            return true;
        }

        public ShipperDetail Apply(ShipperDetail data)
        {
            try {
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
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return null;
            }
        }

        public bool Update(ShipperDetail data)
        {
            try {
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
            catch(Exception e)
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
            try {
                var shipper = _db.ShipperDetails.Where(x => x.ID == id).SingleOrDefault();
                return shipper;
            }
            catch(Exception e)
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
                    .Where(x => (x.Order.StoreId == storeId) && (x.Order.StatusID == WAITING_FOR_SHIPPING))
                    .ToList();
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
