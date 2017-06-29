using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    class ProductTypeRepository
    {
        private EasyShoppingEntities _db = null;
        
        public ProductTypeRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            return _db.ProductTypes.ToList();
        }
    }
}
