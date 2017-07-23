using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class PartnerRepository
    {
        EasyShoppingEntities _db;

        public PartnerRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool IsPartner(int storeId, int id)
        {
            if (_db.Partners.Where(x => (x.StoreID == storeId) && (x.UseID == id) && (x.isWorking == true)).Count() > 0) return true;
            return false;
        }

        public Partner Apply(Partner partner)
        {
            try
            {
                var newPartner = _db.Partners.Add(partner);
                _db.SaveChanges();
                return newPartner;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public bool Edit(Partner partner)
        {
            try
            {
                var newpartner = _db.Partners.Where(x => x.ID == partner.ID).Single();
                newpartner.isWorking = partner.isWorking;
                newpartner.ModifiedDate = partner.ModifiedDate;
                newpartner.ModifiedID = partner.ModifiedID;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                _db.Partners.Where(x => x.ID == id).Single().isWorking = false;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public bool RemoveFromApprove(int? id, int? userId, int? storeId)
        {
            try
            {
                if((userId.HasValue) && (storeId.HasValue) && (id.Value == 0))
                {
                    _db.Partners.Remove(_db.Partners.Where(x => (x.UseID == userId.Value) && (x.StoreID == storeId.Value)).Single());
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    _db.Partners.Remove(_db.Partners.Where(x => x.ID == id.Value).Single());
                    _db.SaveChanges();
                    return true;
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public Partner FindByName(string name)
        {
            var partner = _db.Partners.Include("User").Where(x => x.User.UserName.Equals(name)).Single();
            return partner;
        }

        public bool IsApplied(int userId, int storeId)
        {
            try
            {
                var count = _db.Partners.Where(x => (x.UseID == userId) && (x.StoreID == storeId)).Count();
                if(count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public IEnumerable<Partner> GetList(int storeId)
        {
            try
            {
                var partners = _db.Partners.Where(x => x.StoreID == storeId && x.isWorking == false).ToList();
                return partners;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public Partner FindById(int id)
        {
            try
            {
                var partner = _db.Partners.Where(x => x.ID == id).Single();
                return partner;
            }
            catch
            {
                return null;
            }
        }
    }
}
