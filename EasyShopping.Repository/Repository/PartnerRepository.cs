using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class PartnerRepository
    {
        EasyShoppingEntities _db;

        public PartnerRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool IsPartner(int storeId, int id)
        {
            if (_db.Partners.Where(x => (x.StoreID == storeId) && (x.UseID == id)).Count() > 0) return true;
            return false;
        }
    }
}
