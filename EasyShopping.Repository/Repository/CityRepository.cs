using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    class CityRepository
    {
        private EasyShoppingEntities _db = null;
        public CityRepository()
        {
            _db = new EasyShoppingEntities();
        }
        public IEnumerable<Province> GetAll()
        {
            try
            {
                return _db.Provinces.ToList();
            }
            catch
            {
                return null;
            }
        }
        public Province GetById(int id)
        {
            return _db.Provinces.Where(x => x.Id == id).SingleOrDefault();
        }
    }
}
