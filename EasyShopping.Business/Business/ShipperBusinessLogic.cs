using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.BusinessLogic.Business
{
    public class ShipperBusinessLogic
    {
        private ShipperRepository _repo;
        private UserRepository _user;
        private const int WAITINGFORAPPROVE = 1;
        private const int ACTIVE = 2;

        public ShipperBusinessLogic()
        {
            _repo = new ShipperRepository();
            _user = new UserRepository();
        }

        public bool Apply(ShipperDetailDTO data, string username)
        {
            data.RegDate = DateTime.Now;
            data.StatusId = WAITINGFORAPPROVE;
            data.ShipperId = _user.FindUser(username).ID;
            return _repo.Apply(data.Translate<ShipperDetailDTO, ShipperDetail>());
        }

        public bool Approve(int id)
        {
            var shipper = _repo.GetById(id).Translate<ShipperDetail, ShipperDetailDTO>();
            shipper.StatusId = ACTIVE;
            return _repo.Update(shipper.Translate<ShipperDetailDTO, ShipperDetail>());
        }

        public bool Reject(int id, string username)
        {
            var user = _user.FindUser(username);
            var result = _repo.Reject(id, user.ID);
            return result;
        }

        public IEnumerable<ShippingDetailViewDTO> GetByStoreId(int storeId)
        {
            var result = _repo.GetByStore(storeId);
            return result.Translate<ShippingDetail, ShippingDetailViewDTO>();
        }

        public ShipperDetailDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<ShipperDetail, ShipperDetailDTO>();
        }

        public bool RemoveShipper(int id)
        {
            return _repo.Remove(id);
        }
    }
}
