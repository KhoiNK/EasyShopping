using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System.Collections.Generic;

namespace EasyShopping.BusinessLogic.Business
{
    public class ProductTypeBusiness
    {
        private ProductTypeRepository _repo = null;

        public ProductTypeBusiness()
        {
            _repo = new ProductTypeRepository();
        }

        public IEnumerable<ProductTypeDTO> GetAll()
        {
            return _repo.GetAll().Translate<ProductType, ProductTypeDTO>();
        }
    }
}
