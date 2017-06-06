using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace EasyShopping.BusinessLogic.Business
{
    public class StoreBusinessLogic
    {
        private StoreRepository _repo;
        const int WAITINGFORAPPROVE = 3;
        const int OPEN = 1;
        private UserBusinessLogic _userbusiness = null;

        public StoreBusinessLogic()
        {
            _repo = new StoreRepository();
            _userbusiness = new UserBusinessLogic();
        }

        public Task<StoreDTO> CreateStore(StoreDTO store)
        {
            return Task.Factory.StartNew(() =>
            {
                
                store.CreatedDate = System.DateTime.Now;
                store.ModifiedDate = System.DateTime.Now;
                store.StatusID = WAITINGFORAPPROVE;
                store.UserID = _userbusiness.GetByName(store.UserName).Result.ID;
                _repo.Create(store.Translate<StoreDTO, Store>());
                return store;
            });

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
                if(userid != ownerid)
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
    }
}