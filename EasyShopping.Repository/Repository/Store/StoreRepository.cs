using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    class StoreRepository
    {
        EasyShoppingEntities _db = null;
        const int WAITINGFORAPPROVE = 3;
        const int OPEN = 1;

        public StoreRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public IEnumerable<Store> GetList(int pageSize, int pageIndex)
        {
            int skipped = (pageIndex - 1) * pageSize;
            return _db.Stores
                .Include("User")
                .Include("StoreStatu")
                .ToList()
                .Skip(skipped);
        }

        public Store FindByID(int id)
        {
            return _db.Stores
                .Include("User")
                .Include("StoreStatu")
                .Where(x => x.ID == id)
                .SingleOrDefault();
        }

        public Store Create(Store store)
        {
            Store newstore = new Store();
            newstore = _db.Stores.Add(newstore);
            _db.SaveChanges();
            return FindByID(newstore.ID);
        }


    }
}
