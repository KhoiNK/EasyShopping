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
    public class PartnerBusiness
    {
        private PartnerRepository _repo;
        private UserBusinessLogic _user;
        private StoreRepository _store;
        private MessageRepository _mess;

        private const int MESS_PARTNER = 5;
        private const int MESS_STORE = 3;

        public PartnerBusiness()
        {
            _repo = new PartnerRepository();
            _user = new UserBusinessLogic();
            _store = new StoreRepository();
            _mess = new MessageRepository();
        }

        public bool IsApplied(int storeId, string username)
        {
            var userId = _user.GetByName(username).Result.ID;
            return _repo.IsApplied(userId, storeId);
        }

        public bool Apply(string name, int storeId)
        {
            var partner = new PartnerDTO();
            var user = _user.GetByName(name).Result;
            var store = _store.FindByID(storeId);
            partner.ModifiedDate = DateTime.Now;
            partner.UseID = user.ID;
            partner.StoreID = storeId;
            partner.CreatedDate = DateTime.Now;
            partner.isWorking = false;
            if (_repo.Apply(partner.Translate<PartnerDTO, Partner>()) != null)
            {
                var mess = new Message();
                mess.CreatedDate = DateTime.Now;
                mess.DataID = storeId;
                mess.Description = user.UserName + " just applied to your store " + store.Name;
                mess.IsRead = false;
                mess.MessageType = MESS_PARTNER;
                mess.SentID = store.UserID;
                mess.FromID = user.ID;
                _mess.CreateMessage(mess);
                return true;
            }
            return false;
        }

        public PartnerDTO FindPartner(string name)
        {
            var partner = _repo.FindByName(name);
            return partner.Translate<Partner, PartnerDTO>();
        }

        public bool RemoveApply(string username, int storeId)
        {
            return _repo.RemoveFromApprove(null, _user.GetByName(username).Result.ID, storeId);
        }

        public bool RemoveFromApprovement(int id)
        {
            return _repo.RemoveFromApprove(id, null, null);
        }

        public IEnumerable<PartnerDTO> GetList(int storeId)
        {
            var partners = _repo.GetList(storeId).Translate<Partner, PartnerDTO>();
            return partners;
        }

        public PartnerDTO FindById(int id)
        {
            return _repo.FindById(id).Translate<Partner, PartnerDTO>();
        }

        public bool Approve(int id, string name)
        {
            
            var partner = FindById(id);
            var store = _store.FindByID(partner.StoreID);
            partner.ModifiedID = _user.GetByName(name).Result.ID;
            partner.ModifiedDate = DateTime.Now;
            partner.isWorking = true;
            var mess = new Message();
            mess.CreatedDate = DateTime.Now;
            mess.DataID = partner.StoreID;
            mess.Description = "Your apply to store " + store.Name + " is accepted.";
            mess.IsRead = false;
            mess.MessageType = MESS_STORE;
            mess.SentID = partner.UseID;
            mess.FromID = store.UserID;
            _mess.CreateMessage(mess);
            return _repo.Edit(partner.Translate<PartnerDTO, Partner>());
        }

        public bool Remove(int id)
        {
            return _repo.Remove(id);
        }
    }
}
