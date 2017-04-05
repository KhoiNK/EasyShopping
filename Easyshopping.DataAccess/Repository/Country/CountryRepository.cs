using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easyshopping.DataAccess.Repository.CountryRepo
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
    }
}