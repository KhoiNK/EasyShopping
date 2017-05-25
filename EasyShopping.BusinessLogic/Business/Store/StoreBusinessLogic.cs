using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Repository;

namespace EasyShopping.BusinessLogic.Business
{
    public class StoreBusinessLogic
    {
        private StoreRepository _repo;
        const int WAITINGFORAPPROVE = 3;
        const int OPEN = 1;

        public StoreBusinessLogic()
        {
            _repo = new StoreRepository();
        }

        //public StoreDTO CreateStore(StoreDTO store)
        //{

        //} 
    }
}