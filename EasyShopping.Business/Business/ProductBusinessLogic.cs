using EasyShopping.BusinessLogic.Models;
using System.Collections.Generic;
using EasyShopping.Repository.Repository;
using EasyShopping.Repository.Models.Entity;
using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.CommonMethod;
using System;

namespace EasyShopping.BusinessLogic.Business
{
    public class ProductBusinessLogic
    {
        private ProductRepository _repo;
        private UserRepository _user;
        private PartnerRepository _partner;
        private StoreRepository _store;

        private const int AVAILABLE = 1;
        private const int OUTOFSTOCK = 3;
        private const int WAITINGFORAPPROVE = 2;
        private const string CREATE = "Create";
        private const string DELETE = "Delete";

        public ProductBusinessLogic()
        {
            _repo = new ProductRepository();
            _user = new UserRepository();
            _partner = new PartnerRepository();
            _store = new StoreRepository();
        }
        public ProductDTO Add(ProductDTO data, string userName)
        {
            var userID = _user.FindUser(userName).ID;
            data.ActionLog = "Create";
            if (_partner.IsPartner(data.StoreID, userID))
            {
                data.StatusID = WAITINGFORAPPROVE;
            }
            else
            {
                if (_store.IsOwner(data.StoreID, userID))
                {
                    data.StatusID = AVAILABLE;
                }
                else { return null; }
            }
            data.ProductID = CodeGenerator.RandomString(6);
            data.CreatedDate = System.DateTime.Now;
            data.ModifiedDate = System.DateTime.Now;
            ProductDTO product = _repo.Add(data.Translate<ProductDTO, Product>()).Translate<Product, ProductDTO>();
            return product;
        }

        public IEnumerable<ProductDTO> GetAllByStore(int storeid)
        {
            IEnumerable<ProductDTO> products = _repo.GetList(storeid).Translate<Product, ProductDTO>();
            return products;
        }

        public IEnumerable<ProductViewDTO> GetAllWithUser(string userName)
        {
            var user = _user.FindUser(userName);

            var products = _repo.GetProductWithUserId(user.ID);
            return products.Translate<Product, ProductViewDTO>();
        }

        public IEnumerable<ProductViewDTO> GetAllWithoutUser()
        {
            var result = _repo.GetWithoutUserId();
            return result.Translate<Product, ProductViewDTO>();
        }

        public ProductViewDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<Product, ProductViewDTO>();
        }

        public bool Edit(ProductDTO data, string username)
        {
            var userID = _user.FindUser(username).ID;
            switch (data.ActionLog)
            {
                case CREATE:
                    var product = _repo.GetById(data.ID);
                    product.StatusID = AVAILABLE;
                    return _repo.Edit(product);

                case DELETE:
                    if (_partner.IsPartner(data.StoreID,userID))
                    {
                        data.StatusID = WAITINGFORAPPROVE;
                        return _repo.Edit(data.Translate<ProductDTO, Product>());
                    }
                    return _repo.Remove(data.ID);

                default:
                    if (String.IsNullOrEmpty(data.ThumbailLink) && String.IsNullOrEmpty(data.ThumbailCode))
                    {
                        data.ThumbailLink = _repo.GetById(data.ID).ThumbailLink;
                        data.ThumbailCode = _repo.GetById(data.ID).ThumbailCode;
                    }
                    if (_partner.IsPartner(data.StoreID, userID))
                    {
                        data.StatusID = WAITINGFORAPPROVE;
                    }
                    else
                    {
                        if (_store.IsOwner(data.StoreID, userID))
                        {
                            data.StatusID = AVAILABLE;
                            return _repo.Edit(data.Translate<ProductDTO, Product>());
                        }
                        else { return false; }
                    }
                    return _repo.Edit(data.Translate<ProductDTO, Product>());
            }
        }

        public bool Approve(int id, string name)
        {
            var product = _repo.GetById(id);
            product.StatusID = AVAILABLE;
            return _repo.Edit(product);
        }

        public IEnumerable<ProductDTO> GetApproveList(int id)
        {
            return _repo.GetApproveList(id).Translate<Product, ProductDTO>();
        }

        public IEnumerable<ProductViewDTO> GetByName(string name)
        {
            var result = _repo.GetByName(name).Translate<Product, ProductViewDTO>();
            return result;
        }

        public ProductDTO GetProduct(int id)
        {
            return _repo.GetById(id).Translate<Product, ProductDTO>();
        }

        public bool Remove(int id, string username)
        {
            var product = _repo.GetById(id);
            var userId = _user.FindUser(username).ID;
            if(_partner.IsPartner(product.StoreID, userId))
            {
                product.StatusID = WAITINGFORAPPROVE;
                product.ActionLog = DELETE;
                return _repo.Edit(product);
            }
            return _repo.Remove(id);
        }
    }
}
