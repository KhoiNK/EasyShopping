using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easyshopping.Repository.Repository
{
    public class CountryRepository
    {
        private EasyShoppingEntities _db = null;
        public CountryRepository()
        {
            _db = new EasyShoppingEntities();
        }
        public IEnumerable<Country> GetAll()
        {
            try
            {
                return _db.Countries.ToList();
            }
            catch
            {
                return null;
            }
        }
        public Country GetById(int id)
        {
            return _db.Countries.Where(x => x.Id == id).SingleOrDefault();
        }

        public Country GetByName(string name)
        {
            try {
                var result = _db.Countries.Where(x => x.CommonName.Contains(name.Trim())).SingleOrDefault();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }
    }
}