using EasyShopping.Repository.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace EasyShopping.Repository.Repository
{
    public class StoreRepository
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
                .Include("Ward")
                .Include("District")
                .Include("Country")
                .Include("Province")
                .ToList()
                .Skip(skipped);
        }

        public Store FindByID(int id)
        {
            return _db.Stores
                .Include("User")
                .Include("StoreStatu")
                .Include("Ward")
                .Include("District")
                .Include("Country")
                .Include("Province")
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
