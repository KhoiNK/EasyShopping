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
        private MessageRepository _mess;

        private const int AVAILABLE = 1;
        private const int OUTOFSTOCK = 3;
        private const int WAITINGFORAPPROVE = 2;
        private const string CREATE = "Create";
        private const string DELETE = "Delete";
        private const int MESSAGE_PRODUCT = 2;

        public ProductBusinessLogic()
        {
            _repo = new ProductRepository();
            _user = new UserRepository();
            _partner = new PartnerRepository();
            _store = new StoreRepository();
            _mess = new MessageRepository();
        }
        public ProductDTO Add(ProductDTO data, string userName)
        {
            var user = _user.FindUser(userName); 
            data.ActionLog = "Create";
            bool isPartner = _partner.IsPartner(data.StoreID, user.ID);
            if (isPartner)
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
            if(product != null)
            {
                var store = _store.FindByID(data.StoreID);
                store.LimitProduct = store.LimitProduct - 1;
                _store.Edit(store);
                if (isPartner)
                {
                    var mess = new MessageDTO();
                    mess.Description = user.UserName + " created product " + data.Name;
                    mess.FromID = user.ID;
                    mess.SentID = store.UserID;
                    mess.DataID = data.StoreID;
                    mess.MessageType = MESSAGE_PRODUCT;
                    CreateMessage(mess);
                }
            }
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
            var storeOwnerId = _store.FindByID(data.StoreID).UserID;
            var mess = new MessageDTO();
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
                        mess.Description = user.UserName + " removed product " + data.Name;
                        mess.FromID = user.ID;
                        mess.SentID = storeOwnerId;
                        mess.DataID = data.StoreID;
                        mess.MessageType = MESSAGE_PRODUCT;
                        CreateMessage(mess);
                        return _repo.Edit(data.Translate<ProductDTO, Product>());
                    }
                    var result = _repo.Remove(data.ID);
                    if(result == true)
                    {
                        var store = _store.FindByID(data.StoreID);
                        store.LimitProduct = store.LimitProduct + 1;
                        _store.Edit(store);
                    }
                    return result;

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
                    mess.Description = user.UserName + " removed product " + data.Name;
                    mess.FromID = user.ID;
                    mess.SentID = storeOwnerId;
                    mess.DataID = data.StoreID;
                    mess.MessageType = MESSAGE_PRODUCT;
                    CreateMessage(mess);
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
                var mess = new MessageDTO();
                mess.Description = user.UserName + " removed product " + product.Name;
                mess.FromID = user.ID;
                mess.SentID = product.Store.UserID;
                mess.DataID = product.StoreID;
                mess.MessageType = MESSAGE_PRODUCT;
                CreateMessage(mess);
                return _repo.Edit(product);
            }
            var store = _store.FindByID(product.StoreID);
            store.LimitProduct = store.LimitProduct + 1;
            _store.Edit(store);
            return _repo.Remove(id);
        }

        public void CreateMessage(MessageDTO data)
        {
            var mess = new Message();
            mess.FromID = data.FromID;
            mess.SentID = data.SentID;
            mess.Description = data.Description;
            mess.DataID = data.DataID;
            mess.MessageType = data.MessageType;
            mess.CreatedDate = DateTime.Now;
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
