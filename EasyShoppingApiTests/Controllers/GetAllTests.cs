using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyShopping.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using EasyShopping.Repository.Repository;
using EasyShopping.Repository.Models.Entity;

namespace Products.Tests
{
    [TestClass()]
    public class ProductTests
    {
        ProductRepository _product;
        ProductController _productSrv;
        
        [TestMethod()]
        public void GetProductDetailTest()
        {
            _product = new ProductRepository();
            _productSrv = new ProductController();
            //System.Diagnostics.Debugger.Launch();
            var result = _product.GetById(4);
            //var result = _productSrv.GetDetail(4);
            Assert.AreEqual(4, result.ID);
        }

        [TestMethod()]
        public void GetAllProduct()
        {
            _product = new ProductRepository();
            var result = _product.GetWithoutUserId();
            int act = 0;
            if(result.Count() > 0)
            {
                act = 1;
            }
            var expect = 1;
            Assert.AreEqual(expect, act);
        }

        [TestMethod()]
        public void EditProduct()
        {
            _product = new ProductRepository();
            var editproduct = new Product();
            var product = _product.GetById(4);
            product.Quantity = 30;
            product.ActionLog= "Change Quantity to 30";
            Assert.IsFalse(_product.Edit(editproduct));
            Assert.IsTrue(_product.Edit(product));
        }

        [TestMethod()]
        public void CreateProduct()
        {
            _product = new ProductRepository();
            var newproduct = new Product();
            newproduct.CreatedDate = DateTime.Now;
            newproduct.Description = "this ia a new product";
            newproduct.Height = 20;
            newproduct.ManufacturedCountryID = 237;
            newproduct.ModifiedDate = DateTime.Now;
            newproduct.Name = "Iphone 8";
            newproduct.Price = 10000000;
            newproduct.ProductTypeID = 3;
            newproduct.StatusID = 1;
            newproduct.ThumbailCode = "ABC";
            newproduct.ThumbailLink = "https://cdn2.tgdd.vn/Products/Images/42/88622/iphone-8-1-200x200.jpg";
            newproduct.StoreID = 3;
            newproduct.Weight = 35;
            newproduct.Quantity = 50;
            Assert.IsNotNull(_product.Add(newproduct));
        }


    }
}