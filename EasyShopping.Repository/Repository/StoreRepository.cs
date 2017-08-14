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
        const int REMOVE = 4;

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
                .Where(x => x.StatusID == WAITINGFORAPPROVE)
                .OrderByDescending(x => x.CreatedDate)
                .ToList()
                .Skip(skipped);
            return stores;
        }


        public Store FindByID(int id)
        {
            try
            {
                return _db.Stores
               .Include("User")
               .Include("StoreStatu")
               .Include("Ward")
               .Include("District")
               .Include("Country")
               .Include("Province")
               .Where(x => (x.ID == id) && (x.StatusID != REMOVE))
               .SingleOrDefault();
            }
            catch
            {
                return null;
            }

        }

        public Store Create(Store data)
        {
            try
            {
                var store = new Store();
                store = data;
                if (store.DistrictId == 0)
                {
                    store.DistrictId = null;
                }
                store.WardId = null;
                store = _db.Stores.Add(store);
                _db.SaveChanges();
                return FindByID(store.ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.InnerException.InnerException.Message);
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var store = _db.Stores.Where(x => x.ID == id).Single();
                var products = _db.Products.Where(s => s.StoreID == store.ID).ToList();
                foreach (var p in products)
                {
                    p.StatusID = REMOVE;
                }
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
                FindByID(id).StatusID = OPEN;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
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
                .Where(x => x.Name.Contains(name) && ((x.StatusID == OPEN) || (x.StatusID == WAITINGFORAPPROVE)))
                .ToList();
        }

        public IEnumerable<Store> GetByUserId(int id)
        {
            IEnumerable<Store> stores = _db.Stores.Include("User")
                .Include("StoreStatu")
                .Include("Ward")
                .Include("District")
                .Include("Country")
                .Include("Province")
                .Where(x => (x.UserID == id) && ((x.StatusID == WAITINGFORAPPROVE)||(x.StatusID == OPEN))).ToList();
            return stores;
        }

        public bool Edit(Store store)
        {
            try
            {
                Store editStore = _db.Stores.Where(x => x.ID == store.ID).Single();
                editStore.Address = store.Address;
                editStore.BankAccount = store.BankAccount;
                editStore.CityId = store.CityId;
                editStore.CountryId = store.CountryId;
                editStore.CreatedDate = store.CreatedDate;
                editStore.Description = store.Description;
                editStore.DistrictId = store.DistrictId;
                editStore.ImgLink = store.ImgLink;
                editStore.LatX = store.LatX;
                editStore.LatY = store.LatY;
                editStore.ModifiedByID = store.ModifiedByID;
                editStore.ModifiedDate = DateTime.Now;
                editStore.Name = store.Name;
                editStore.StatusID = store.StatusID;
                editStore.TaxCode = store.TaxCode;
                editStore.RecruitmentMessage = store.RecruitmentMessage;
                editStore.RequiredDeposit = store.RequiredDeposit;
                editStore.IsRecruiting = store.IsRecruiting;
                editStore.LimitProduct = store.LimitProduct;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public bool IsOwner(int id, int userId)
        {
            if (_db.Stores.Where(x => (x.UserID == userId) && (x.ID == id)).Count() > 0) { return true; }
            return false;
        }

        public bool IsPartner(int id, int storeId)
        {
            try
            {
                if (_db.Partners.Where(x => (x.StoreID == storeId) && (x.UseID == id) && (x.isWorking == true)).Single() != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public IEnumerable<Order> GetStoreOrders(int id)
        {
            var orders = _db.Orders.Where(x => x.StoreId == id).ToList();
            return orders;
        }

    }
}
