using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyShopping.BusinessLogic.Business
{
    public class StoreBusinessLogic
    {
        private StoreRepository _repo;
        const int WAITINGFORAPPROVE = 3;
        const int OPEN = 1;
        private UserBusinessLogic _userbusiness = null;
        private ProductBusinessLogic _productbusiness = null;
        private CityRepository _city;
        private CountryRepository _country;
        private DistrictRepository _district;

        public StoreBusinessLogic()
        {
            _repo = new StoreRepository();
            _userbusiness = new UserBusinessLogic();
            _country = new CountryRepository();
            _city = new CityRepository();
            _district = new DistrictRepository();
            _productbusiness = new ProductBusinessLogic();
        }

        public StoreDTO CreateStore(StoreDTO store)
        {
            store.CreatedDate = System.DateTime.Now;
            store.ModifiedDate = System.DateTime.Now;
            store.StatusID = WAITINGFORAPPROVE;
            store.UserID = _userbusiness.GetByName(store.UserName).Result.ID;
            store.CityId = _city.GetByName(store.City).Id;
            store.CountryId = _country.GetByName(store.Country).Id;
            store.DistrictId = _district.GetByName(store.District).Id;
            store.ModifiedByID = _userbusiness.GetByName(store.UserName).Result.ID;

            store = _repo.Create(BusinessTranslators.ToStoreEntity(store)).Translate<Store, StoreDTO>();
            return store;
        }

        public Task<StoreDTO> Get(int id)
        {
            return Task.Factory.StartNew(() =>
            {

                return _repo.FindByID(id).Translate<Store, StoreDTO>();
            });
        }

        public Task<bool> Delete(int id, string username)
        {
            return Task.Factory.StartNew(() =>
            {
                var userid = _userbusiness.GetByName(username).Result.ID;
                var ownerid = _repo.FindByID(id).UserID;
                if (userid != ownerid)
                {
                    return false;
                }
                if (_repo.Delete(id))
                {
                    return true;
                }
                return false;
            });
        }

        public bool ApproveStore(int id)
        {
            return _repo.Approve(id);
        }

        public IEnumerable<StoreDTO> GetAll(int pagesize, int page)
        {
            IEnumerable<StoreDTO> store = _repo.GetList(pagesize, page).Translate<Store, StoreDTO>();

            foreach (var s in store)
            {
                s.ModifiedUser = _userbusiness.GetByID(s.ModifiedByID).UserName;
            }

            return store;
        }

        public IEnumerable<StoreDTO> GetByName(string searchkey)
        {
            IEnumerable<StoreDTO> store = _repo.GetByName(searchkey).Translate<Store, StoreDTO>();
            return store;
        }

        public Task<IEnumerable<StoreDTO>> GetByUserId(string username)
        {
            return Task.Factory.StartNew(() =>
            {
                var user = _userbusiness.GetByName(username).Result;
                IEnumerable<StoreDTO> stores = _repo.GetByUserId(user.ID).Translate<Store, StoreDTO>();
                foreach (var s in stores)
                {
                    s.Products = _productbusiness.GetAllByStore(s.ID);
                }

                return stores;
            });

        }

        public StoreDTO GetById(int id)
        {
            StoreDTO store = _repo.FindByID(id).Translate<Store, StoreDTO>();
            store.Products = _productbusiness.GetAllByStore(store.ID);
            return store;
        }

        public bool Put(StoreDTO store)
        {
            store.ModifiedDate = DateTime.Now;
            var result = _repo.Put(BusinessTranslators.ToStoreEntity(store));
            return result;
        }

        public bool IsOwner(int storeId, string username)
        {
            var user = _userbusiness.GetByName(username);
            return _repo.IsOwner(storeId, user.Result.ID);
        }

        public bool IsAllowed(string name, int storeId)
        {
            var user = _userbusiness.GetByName(name).Result;
            if(_repo.IsOwner(storeId, user.ID))
            {
                return true;
            }
            var result = _repo.IsPartner(user.ID, storeId);
            return result;
        }
    }
}