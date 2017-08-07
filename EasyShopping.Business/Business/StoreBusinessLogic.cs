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
        private const int WAITINGFORAPPROVE = 3;
        private const int OPEN = 1;
        private const int ADMIN = 1;
        private const int PACKAGE1 = 10;
        private const int PACKAGE2 = 30;
        private const int PACKAGE3 = 50;
        private const int PACKAGE4 = 100;

        private UserBusinessLogic _userbusiness = null;
        private ProductBusinessLogic _productbusiness = null;
        private CityRepository _city;
        private CountryRepository _country;
        private DistrictRepository _district;
        private MessageBusinessLogic _mess;

        public StoreBusinessLogic()
        {
            _repo = new StoreRepository();
            _userbusiness = new UserBusinessLogic();
            _country = new CountryRepository();
            _city = new CityRepository();
            _district = new DistrictRepository();
            _productbusiness = new ProductBusinessLogic();
            _mess = new MessageBusinessLogic();
        }

        public StoreDTO CreateStore(StoreDTO store)
        {
            var userId = _userbusiness.GetByName(store.UserName).Result.ID;
            store.CreatedDate = System.DateTime.Now;
            store.ModifiedDate = System.DateTime.Now;
            store.StatusID = WAITINGFORAPPROVE;
            store.UserID = userId;
            store.CityId = _city.GetByName(store.City).Id;
            store.CountryId = _country.GetByName(store.Country).Id;
            var DisString = store.District.Split('.');
            if (DisString.Length > 1) { store.DistrictId = _district.GetByName(DisString[1]).Id; }
            else if (DisString.Length == 1) { store.DistrictId = _district.GetByName(DisString[0]).Id; }
            store.ModifiedByID = userId;
            store.LimitProduct = PACKAGE1;
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
            var result = _repo.Approve(id);
            var store = _repo.FindByID(id);
            var mess = new MessageDTO();
            mess.FromID = ADMIN;
            mess.SentID = store.UserID;
            mess.Description = "Your store " + store.Name + " is activated.";
            _mess.CreateMessage(mess);
            return result;
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

        public bool Put(StoreDTO store, string name)
        {
            var userId = _userbusiness.GetByName(store.UserName).Result.ID;
            store.ModifiedDate = System.DateTime.Now;
            store.CityId = _city.GetByName(store.City).Id;
            store.CountryId = _country.GetByName(store.Country).Id;
            var DisString = store.District.Split('.');
            if (DisString.Length > 1) { store.DistrictId = _district.GetByName(DisString[1]).Id; }
            else if (DisString.Length == 1) { store.DistrictId = _district.GetByName(DisString[0]).Id; }
            store.ModifiedByID = userId;
            if (string.IsNullOrEmpty(store.ImgLink))
            {
                store.ImgLink = _repo.FindByID(store.ID).ImgLink;
            }
            var result = _repo.Edit(BusinessTranslators.ToStoreEntity(store));
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

        public bool UpgradeStore(int storeid, int packageid)
        {
            var store = _repo.FindByID(storeid);

            switch (packageid)
            {
                case 2:
                    store.LimitProduct = PACKAGE2 + store.LimitProduct;
                    return _repo.Edit(store);
                case 3:
                    store.LimitProduct = PACKAGE3 + store.LimitProduct;
                    return _repo.Edit(store);
                case 4:
                    store.LimitProduct = PACKAGE4 + store.LimitProduct;
                    return _repo.Edit(store);
                default:
                    store.LimitProduct = PACKAGE1 + store.LimitProduct;
                    return _repo.Edit(store);
            }
        }
    }
}