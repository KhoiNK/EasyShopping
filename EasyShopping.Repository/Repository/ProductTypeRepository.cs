using EasyShopping.Repository.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace EasyShopping.Repository.Repository
{
    public class ProductTypeRepository
    {
        private EasyShoppingEntities _db = null;
        
        public ProductTypeRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public IEnumerable<ProductType> GetAll()
        {
            return _db.ProductTypes.ToList();
        }
    }
}
