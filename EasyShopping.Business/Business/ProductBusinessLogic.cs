using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using EasyShopping.Repository.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyShopping.Repository.Models.Entity;

namespace EasyShopping.BusinessLogic.Business
{
    public class ProductBusinessLogic
    {
        private ProductRepository _repo;

        public ProductBusinessLogic()
        {
            _repo = new ProductRepository();
        }
        public ProductDTO Add(ProductDTO data, IList<string> filepaths)
        {
            ProductDTO product = _repo.Add(data.Translate<ProductDTO, Product>()).Translate<Product, ProductDTO>();
            if(filepaths.Count > 0)
            {
                foreach (var img in filepaths)
                {
                    _repo.AddImage(img, product.ID);
                }
            }
            product.Images = _repo.GetImage(product.ID).Translate<Image, ImageDTO>();
            return product;
        }

        public IList<ProductDTO> GetAll(int storeid)
        {
            IList<ProductDTO> products = _repo.GetList(storeid).Translate<Product, ProductDTO>();
            return products;
        }
    }
}
