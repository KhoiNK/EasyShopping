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
        private const int REMOVED = 4;
        private const int WAITINGFORAPPROVE = 2;

        public ProductRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.Include("Images")
                .Include("ProductType")
                .Include("ProductStatu")
                .Include("Store")
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        public IEnumerable<Product> GetList(int storeid)
        {
            return _db.Products
                .Include("Images")
                .Include("ProductType")
                .Include("ProductStatu")
                .Include("Store")
                .Where(x=> x.StoreID == storeid)
                .ToList();
        }

        public Product Add(Product data)
        {
            Product product = new Product();
            product = data;
            _db.Products.Add(product);
            _db.SaveChanges();
            return product;
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

        public Product GetById(int id)
        {
            Product product = _db.Products
                .Include("ProductType")
                .Include("ProductStatu")
                .Include("Country")
                .Include("Store")
                .Where(x => x.ID == id).Single();
            return product;
        }

        public bool Remove(int id)
        {
            var product = GetById(id);
            product.StatusID = REMOVED;
            _db.SaveChanges();
            return true;
        }

        public bool Edit(Product data)
        {
            try
            {
                var product = GetById(data.ID);
                if (String.IsNullOrEmpty(data.ThumbailLink) && String.IsNullOrEmpty(data.ThumbailCode))
                {
                    var thumbailLink = product.ThumbailLink;
                    var thumbailCode = product.ThumbailCode;
                    product = data;
                    product.ThumbailLink = thumbailLink;
                    product.ThumbailCode = thumbailCode;
                    _db.SaveChanges();
                    return true;
                }
                product = data;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            
        }

        public IEnumerable<Product> GetByName(string name)
        {
            var products = _db.Products.Where(x => x.Name.Contains(name)).ToList();
            return products;
        }

        public IEnumerable<Product> GetApproveList()
        {
            var products = _db.Products.Where(x => x.StatusID == WAITINGFORAPPROVE).ToList();
            return products;
        }
    }
}
