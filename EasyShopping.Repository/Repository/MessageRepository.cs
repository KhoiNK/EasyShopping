using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyShopping.Repository.Repository
{
    public class MessageRepository
    {
        EasyShoppingEntities _db;
        public MessageRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public Message CreateMessage(Message mess)
        {
            try {
                var message = new Message();
                message = mess;
                message = _db.Messages.Add(message);
                _db.SaveChanges();
                return message;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Message> Get(int userId)
        {
            try {
                var result = _db.Messages
                    .Include("User")
                    .Include("User1")
                    .Where(x => x.SentID == userId)
                    .OrderByDescending(x=>x.CreatedDate)
                    .ToList();
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public IEnumerable<Message> GetThumbail(int userId)
        {
            try
            {
                var result = _db.Messages
                    .Include("User")
                    .Include("User1")
                    .Where(x => x.SentID == userId)
                    .Take(5)
                    .OrderByDescending(x=>x.CreatedDate)
                    .ToList();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public Message GetDetail(int id)
        {
            try {
                var result = _db.Messages
                    .Include("User")
                    .Include("User1")
                    .Where(x => x.ID == id)
                    .SingleOrDefault();
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public bool MarkAsRead(int id)
        {
            try {
                var message = _db.Messages.Where(x => x.ID == id).SingleOrDefault();
                message.IsRead = true;
                _db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }

        public int CountUnread(int userId)
        {
            return _db.Messages.Where(x => x.SentID == userId).Count();
        }
    }
}
