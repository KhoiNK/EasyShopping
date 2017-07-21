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
        public ShipperRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool Reject(int id, int userId)
        {
            var shipper = _db.ShippingDetails.Where(x => x.ID == id).SingleOrDefault();
            shipper.ModifiedID = userId;
            shipper.ModifiedDate = DateTime.Now;
            _db.SaveChanges();
            return true;
        }
    }
}
