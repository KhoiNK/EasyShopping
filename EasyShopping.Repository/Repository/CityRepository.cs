using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class CityRepository
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

        public Province GetByName(string name)
        {
            try
            {
                var result = _db.Provinces.Where(x => x.Name.Contains(name)).Single();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.InnerException.Message);
                return null;
            }
        }
    }
}
