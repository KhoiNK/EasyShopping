using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class RecruitmentRepository
    {
        private EasyShoppingEntities _db = null;
        public RecruitmentRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool Create(Recruitment data)
        {
            try
            {
                var recruit = new Recruitment();
                recruit = data;
                _db.Recruitments.Add(recruit);
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool Edit(Recruitment data)
        {
            try {
                var recruit = _db.Recruitments.Where(x => x.ID == data.ID).Single();
                recruit.Description = data.Description;
                recruit.Requirement = data.Requirement;
                recruit.StoreId = data.StoreId;
                _db.SaveChanges();
                return true;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public Recruitment GetByStore(int storeId)
        {
            try {
                var result = _db.Recruitments.Where(x => x.StoreId == storeId).Single();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
