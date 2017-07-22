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
        PartnerRepository _repo;
        UserBusinessLogic _user;
        public PartnerBusiness()
        {
            _repo = new PartnerRepository();
            _user = new UserBusinessLogic();
        }

        public bool Apply(string name, int storeId)
        {
            var partner = new PartnerDTO();
            partner.ModifiedDate = DateTime.Now;
            partner.UseID = _user.GetByName(name).Result.ID;
            partner.StoreID = storeId;
            partner.CreatedDate = DateTime.Now;
            partner.isWorking = false;
            if (_repo.Apply(partner.Translate<PartnerDTO, Partner>()) != null)
            {
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
            partner.ModifiedID = _user.GetByName(name).Result.ID;
            partner.ModifiedDate = DateTime.Now;
            return _repo.Eidt(partner.Translate<PartnerDTO, Partner>());
        }
    }
}
