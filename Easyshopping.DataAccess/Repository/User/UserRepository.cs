using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easyshopping.DataAccess.Repository.Users
{
    public class UserRepository
    {
        EasyShoppingEntities _db = null;
        public UserRepository()
        {
            _db = new EasyShoppingEntities();
        }
        
        public User FindUser(string user_name, string password, string email)
        {
            try
            {
                return _db.Users.SingleOrDefault(x => (x.Email.Equals(email) || x.UserName.Equals(user_name))
                                                    && x.PassWord.Equals(password));
            }
            catch
            {
                return null;
            }
        }

        public User FindUserByID(int id)
        {
            try
            {
                return _db.Users.SingleOrDefault(x => x.ID == id);
            }
            catch
            {
                return null;
            }
            
        }

        public IEnumerable<User> GetListUser()
        {
            try
            {
                return _db.Users.ToList();
            }
            catch
            {
                return null;
            }
        }

        public User AddUser(User user)
        {
            try
            {
                return _db.Users.Add(user);
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveUser(int id)
        {
            if(FindUserByID(id) == null) { return false; }
            try
            {
                FindUserByID(id).StatusID = 3;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}