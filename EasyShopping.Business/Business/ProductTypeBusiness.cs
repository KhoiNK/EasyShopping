using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System.Collections.Generic;

namespace EasyShopping.BusinessLogic.Business
{
    public class ProductTypeBusiness
    {
        private ProductTypeRepository _repo = null;
        private ProductBusinessLogic _product;

        public ProductTypeBusiness()
        {
            _repo = new ProductTypeRepository();
            _product = new ProductBusinessLogic();
        }

        public IEnumerable<ProductTypeDTO> GetAll()
        {
            return _repo.GetAll().Translate<ProductType, ProductTypeDTO>();
        }

        public IEnumerable<ProductTypeViewDTO> GetWithTarget()
        {
            var types = _repo.GetWithTarget().Translate<ProductType, ProductTypeViewDTO>();
            foreach(var type in types)
            {
                type.Products = _product.GetWithType(type.ID);
            }
            return types;
        }
    }
}
