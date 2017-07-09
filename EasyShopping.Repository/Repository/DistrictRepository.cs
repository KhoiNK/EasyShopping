using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class DistrictRepository
    {
        private EasyShoppingEntities _db = null;
        public DistrictRepository()
        {
            _db = new EasyShoppingEntities();
        }
        public IEnumerable<District> GetAll()
        {
            try
            {
                return _db.Districts.ToList();
            }
            catch
            {
                return null;
            }
        }
        public District GetById(int id)
        {
            return _db.Districts.Where(x => x.Id == id).SingleOrDefault();
        }

        public District GetByName(string name)
        {
            var result = _db.Districts.Where(x => x.Name.Contains(name)).Single();
            return result;
        }
    }
}
