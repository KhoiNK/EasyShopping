using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Generic;

namespace EasyShopping.BusinessLogic.Business
{
    public class MessageBusinessLogic
    {
        MessageRepository _repo;
        UserRepository _user;

        public MessageBusinessLogic()
        {
            _repo = new MessageRepository();
            _user = new UserRepository();
        }

        public MessageDTO CreateMessage(MessageDTO message)
        {
            var mess = new Message();
            mess.CreatedDate = DateTime.Now;
            mess.Description = message.Description;
            mess.FromID = message.FromID;
            mess.IsRead = false;
            mess.SentID = message.SentID;
            mess.MessageType = message.MessageType;
            mess.DataID = message.DataID;
            var result = _repo.CreateMessage(mess);
            return result.Translate<Message, MessageDTO>();
        }

        public IEnumerable<MessageDTO> GetByUserId(string name)
        {
            var id = _user.FindUser(name).ID;
            var result = _repo.Get(id);
            return result.Translate<Message, MessageDTO>();
        }

        public IEnumerable<MessageDTO> GetThumbail(string name)
        {
            var id = _user.FindUser(name).ID;
            var result = _repo.GetThumbail(id);
            return result.Translate<Message, MessageDTO>();
        }

        public bool MarkAsRead(int id)
        {
            return _repo.MarkAsRead(id);
        }

        public int CountUnread(string name)
        {
            return _repo.CountUnread(_user.FindUser(name).ID);
        }

        public MessageDTO GetDetail(int id)
        {
            return _repo.GetDetail(id).Translate<Message, MessageDTO>();
        }
    }
}
