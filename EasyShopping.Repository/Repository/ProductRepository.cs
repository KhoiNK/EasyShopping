using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class ProductRepository
    {
        private EasyShoppingEntities _db = null;

        public ProductRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public IEnumerable<Product> GetList(int storeid)
        {
            return _db.Products
                .Include("Images")
                .Include("ProductType")
                .Include("ProductStatu")
                .Where(x=> x.StoreID == storeid)
                .ToList();
        }

        public Product Add(Product data)
        {
            Product product = new Product();
            product = data;
            return _db.Products.Add(data);
        }

        public bool AddImage(string path, int productId)
        {
            Image image = new Image();
            image.Link = path;
            image.ProductID = productId;
            _db.Images.Add(image);
            return true;
        }

        public IEnumerable<Image> GetImage(int id)
        {
            try
            {
                return _db.Images.Where(x => x.ProductID == id).ToList();
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
