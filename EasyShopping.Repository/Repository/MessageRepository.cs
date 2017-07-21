using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.Repository.Repository
{
    public class MessageRepository
    {
        EasyShoppingEntities _db;
        public MessageRepository()
        {
            _db = new EasyShoppingEntities();
        }

        public bool CreateMessage(Message mess)
        {
            try {
                _db.Messages.Add(mess);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Message> Get(int userId)
        {
            try {
                var result = _db.Messages.Where(x => x.SentID == userId).ToList();
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.InnerException);
                return null;
            }
        }

        public Message GetDetail(int id)
        {
            try {
                var result = _db.Messages.Where(x => x.ID == id).SingleOrDefault();
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
    }
}
