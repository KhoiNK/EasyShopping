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

        public IEnumerable<Product> GetList(int storeid)
        {
            return _db.Products
                .Include("Images")
                .Include("ProductType")
                .Include("ProductStatu")
                .Include("Store")
                .Where(x => x.StoreID == storeid)
                .ToList();
        }

        public IEnumerable<Product> GetProductWithUserId(int userId)
        {
            var target = _db.Targets.Where(x => x.UserId == userId).OrderByDescending(x => x.Count).ToArray();
            if (target.Length == 0)
            {
                var result = _db.Products.Where(x => x.StatusID != REMOVED).Take(30).ToList();
                return result;
            }
            if (target.Length < 5)
            {
                return GetWithTarget(target);
            }
            else
            {
                return GetWithTarget(target);
            }
        }

        public IEnumerable<Product> GetWithoutUserId()
        {
            var target = _db.Targets.OrderByDescending(x => x.Count).ToArray();

            if (target.Length == 0)
            {
                var result = _db.Products.Where(x => x.StatusID != REMOVED).Take(30).ToList();
                return result;
            }
            if (target.Length < 5)
            {
                return GetWithTarget(target);
            }
            else
            {
                return GetWithTarget(target);
            }
        }

        public IEnumerable<Product> GetWithTarget(Target[] target)
        {
            int[] prodTypeIDs = target.Select(t => t.ProductTypeId.Value).ToArray();
            var rnd = new Random();
            var products = _db.Products
                                .Include("ProductType")
                                .Include("ProductStatu")
                                .Include("Country")
                                .Include("Store")
                                .Where(x => prodTypeIDs.Contains(x.ProductTypeID.Value) && x.StatusID != REMOVED)
                                .OrderByDescending(x=>x.CreatedDate)
                                .Take(20)
                                .ToList();
            return products;
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
            _db.SaveChanges();
            return true;
        }

        public IEnumerable<Image> GetImage(int id)
        {
            try
            {
                return _db.Images.Where(x => x.ProductID == id).ToList();
            }
            catch (Exception e)
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
                var product = _db.Products.Where(x => x.ID == data.ID).Single();
                product.ActionLog = data.ActionLog;
                product.CreatedDate = data.CreatedDate;
                product.Description = data.Description;
                product.Height = data.Height;
                product.ManufacturedCountryID = data.ManufacturedCountryID;
                product.ModifiedDate = DateTime.Now;
                product.Name = data.Name;
                product.Price = data.Price;
                product.ProductID = data.ProductID;
                product.ProductTypeID = data.ProductTypeID;
                product.Quantity = data.Quantity;
                product.StatusID = data.StatusID;
                product.StoreID = data.StoreID;
                product.ThumbailCode = data.ThumbailCode;
                product.ThumbailLink = data.ThumbailLink;
                product.Weight = data.Weight;

                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

        }

        public IEnumerable<Product> GetByName(string name)
        {
            try {
                var products = _db.Products.Where(x => x.Name.Contains(name) && (x.Quantity > 0)).Take(5).ToList();
                
                return products;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public IEnumerable<Product> GetApproveList(int id)
        {
            var store = _db.Stores.Where(x => x.ID == id).Single();
            var products = store.Products.Where(x => x.StatusID == WAITINGFORAPPROVE).ToList();
            return products;
        }

        
    }
}
