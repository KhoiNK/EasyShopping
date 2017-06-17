using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyShopping.Repository.Repository
{
    public class StoreRepository
    {
        EasyShoppingEntities _db = null;
        const int WAITINGFORAPPROVE = 3;
        const int OPEN = 1;
        const int CLOSED = 2;
        public StoreRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public IEnumerable<Store> GetList(int pageSize, int pageIndex)
        {
            int skipped = (pageIndex - 1) * pageSize;
            IEnumerable<Store> stores = _db.Stores
                .Include("User")
                .Include("StoreStatu")
                .Include("Ward")
                .Include("District")
                .Include("Country")
                .Include("Province")
                .Include("Products")
                .ToList()
                .Skip(skipped);
            return stores;
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
                .Include("Products")
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

        public bool Delete(int id)
        {
            try
            {
                var store = _db.Stores.Where(x => x.ID == id).Single();
                store.StatusID = CLOSED;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool Approve(int id)
        {
            try
            {
                FindByID(id).StatusID = 1;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            } 
        }

        public IEnumerable<Store> GetByName(string name)
        {
            return _db.Stores.Include("User")
                .Include("StoreStatu")
                .Include("Ward")
                .Include("District")
                .Include("Country")
                .Include("Province")
                .Where(x => x.Name.Equals(name))
                .ToList();
        }

        public IEnumerable<Store> GetByUserId(int id)
        {
            return _db.Stores.Where(x => x.UserID == id).ToList();
        }
    }
}
