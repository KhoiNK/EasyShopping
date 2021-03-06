﻿using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.BusinessLogic.Business
{
    public class ShipperBusinessLogic
    {
        private ShipperRepository _repo;
        private UserRepository _user;
        private MessageRepository _mess;
        private const int WAITINGFORAPPROVE = 1;
        private const int ACTIVE = 2;

        public ShipperBusinessLogic()
        {
            _repo = new ShipperRepository();
            _user = new UserRepository();
            _mess = new MessageRepository();
        }

        public ShipperDetailDTO Apply(ShipperDetailDTO data, string username)
        {
            data.RegDate = DateTime.Now;
            data.StatusId = WAITINGFORAPPROVE;
            data.ShipperId = _user.FindUser(username).ID;
            data.Deposit = 0;
            data.Total = 0;
            return _repo.Apply(data.Translate<ShipperDetailDTO, ShipperDetail>()).Translate<ShipperDetail, ShipperDetailDTO>();
        }

        public bool Approve(int id)
        {
            var shipper = _repo.GetById(id).Translate<ShipperDetail, ShipperDetailDTO>();
            shipper.StatusId = ACTIVE;
            var mess = new Message();
            mess.DataID = null;
            mess.Description = "You have been processed to be a shipper.";
            mess.FromID = 1;
            mess.IsRead = false;
            mess.MessageType = null;
            mess.SentID = shipper.ShipperId;
            mess.CreatedDate = DateTime.Now;
            _mess.CreateMessage(mess);
            return _repo.Update(shipper.Translate<ShipperDetailDTO, ShipperDetail>());
        }

        public bool Reject(int id)
        {
            var result = _repo.Reject(id);
            return result;
        }

        public IEnumerable<ShippingDetailViewDTO> GetByStoreId(int storeId)
        {
            var result = _repo.GetByStore(storeId);
            return result.Translate<ShippingDetail, ShippingDetailViewDTO>();
        }

        public ShipperDetailDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<ShipperDetail, ShipperDetailDTO>();
        }

        public bool RemoveShipper(int id)
        {
            return _repo.Remove(id);
        }

        public bool IsShipper(string name)
        {
            int userID = _user.FindUser(name).ID;
            return _repo.IsShipper(userID);
        }

        public bool IsApplied(string name)
        {
            int userID = _user.FindUser(name).ID;
            return _repo.IsApplied(userID);
        }

        public bool BuyPackage(ShipperDetailDTO data, string name)
        {
            var userId = _user.FindUser(name).ID;
            var shipper = new ShipperDetail();
            if(userId != 1)
            {
                shipper = _repo.GetByUserId(userId);
            }
            else
            {
                shipper = _repo.GetByUserId(data.ShipperId.Value);
            }
            shipper.Deposit = shipper.Deposit.Value + data.Deposit;
            shipper.RecentBalance = shipper.RecentBalance + data.Deposit;
            shipper.Total = shipper.Total.Value + data.Total;
            var result = _repo.Update(shipper);
            return result;
        }

        public IEnumerable<ShipperDetailDTO> GetAll(int pageIndex, int pageSize)
        {
            var result = _repo.GetAll(pageSize, pageIndex).Translate<ShipperDetail, ShipperDetailDTO>();
            return result;
        }

        public ShipperDetailDTO GetByUserID(string name)
        {
            var userID = _user.FindUser(name).ID;
            var result = _repo.GetByUserId(userID);
            return result.Translate<ShipperDetail, ShipperDetailDTO>();
        }
    }
}
