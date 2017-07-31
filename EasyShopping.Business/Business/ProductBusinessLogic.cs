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
        private MessageBusinessLogic _mess;

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
            _mess = new MessageBusinessLogic();
        }
        public ProductDTO Add(ProductDTO data, string userName)
        {
            var user = _user.FindUser(userName); 
            data.ActionLog = "Create";
            if (_partner.IsPartner(data.StoreID, user.ID))
            {
                data.StatusID = WAITINGFORAPPROVE;

            }
            else
            {
                if (_store.IsOwner(data.StoreID, user.ID))
                {
                    data.StatusID = AVAILABLE;
                }
                else { return null; }
            }
            data.ProductID = CodeGenerator.RandomString(6);
            data.CreatedDate = System.DateTime.Now;
            data.ModifiedDate = System.DateTime.Now;
            ProductDTO product = _repo.Add(data.Translate<ProductDTO, Product>()).Translate<Product, ProductDTO>();
            string des = user.UserName + " created product " + data.Name; 
            CreateMessage(user.ID, _store.FindByID(data.StoreID).UserID, des);
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
            var user = _user.FindUser(username);
            string des = "";
            switch (data.ActionLog)
            {
                case CREATE:
                    var product = _repo.GetById(data.ID);
                    product.StatusID = AVAILABLE;
                    return _repo.Edit(product);

                case DELETE:
                    if (_partner.IsPartner(data.StoreID,user.ID))
                    {
                        data.StatusID = WAITINGFORAPPROVE;
                        des = user.UserName + " deleted product " + data.Name;
                        CreateMessage(user.ID, _store.FindByID(data.StoreID).UserID, des);
                        return _repo.Edit(data.Translate<ProductDTO, Product>());
                    }
                    return _repo.Remove(data.ID);

                default:
                    if (String.IsNullOrEmpty(data.ThumbailLink) && String.IsNullOrEmpty(data.ThumbailCode))
                    {
                        data.ThumbailLink = _repo.GetById(data.ID).ThumbailLink;
                        data.ThumbailCode = _repo.GetById(data.ID).ThumbailCode;
                    }
                    if (_partner.IsPartner(data.StoreID, user.ID))
                    {
                        data.StatusID = WAITINGFORAPPROVE;
                    }
                    else
                    {
                        if (_store.IsOwner(data.StoreID, user.ID))
                        {
                            data.StatusID = AVAILABLE;
                            return _repo.Edit(data.Translate<ProductDTO, Product>());
                        }
                        else { return false; }
                    }
                    des = user.UserName + " edited product " + data.Name;
                    CreateMessage(user.ID, _store.FindByID(data.StoreID).UserID, des);
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
            var user = _user.FindUser(username);
            if(_partner.IsPartner(product.StoreID, user.ID))
            {
                product.StatusID = WAITINGFORAPPROVE;
                product.ActionLog = DELETE;
                string des = user.UserName + " removed product " + product.Name;
                CreateMessage(user.ID, _store.FindByID(product.StoreID).UserID, des);
                return _repo.Edit(product);
            }
            return _repo.Remove(id);
        }

        public void CreateMessage(int fromId, int toId, string des)
        {
            var mess = new MessageDTO();
            mess.FromID = fromId;
            mess.SentID = toId;
            mess.Description = des;
            _mess.CreateMessage(mess);
        }

        public IEnumerable<ProductViewDTO> GetWithType(int id)
        {
            var result = _repo.GetWithTypeId(id).Translate<Product, ProductViewDTO>();
            return result;
        }

        public int GetQuantity(int id)
        {
            return _repo.GetQuantity(id);
        }
    }
}
