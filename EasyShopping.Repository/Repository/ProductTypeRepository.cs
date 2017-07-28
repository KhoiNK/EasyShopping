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

        public IEnumerable<ProductType> GetWithTarget()
        {
            int[] targets = _db.Targets.Select(x=>x.ProductTypeId.Value).Take(5).ToArray();
            var products = _db.ProductTypes.Where(x => targets.Contains(x.ID));
            return products;
        }
    }
}
