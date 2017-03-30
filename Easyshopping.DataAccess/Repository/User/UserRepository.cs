using Easyshopping.DataAccess.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Easyshopping.DataAccess.Repository.Users
{
    public class UserRepository
    {
        private EasyShoppingEntities _db = null;
        private UserManager<IdentityUser> _usermanger;
        public UserRepository()
        {
            _db = new EasyShoppingEntities();
            _usermanger = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));
        }

        public User FindUser(string user_name, string password)
        {
            try
            {
                User user = _db.Users.SingleOrDefault(x => ((x.UserName.Equals(user_name.Trim())) || (x.Email.Equals(user_name.Trim())))
                                                && (x.PassWord.Equals(password.Trim())));
                return user;
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
                User newuser = _db.Users.Add(user);
                _db.SaveChanges();
                return newuser;
            }
            catch
            {
                return null;
            }
        }

        public bool RemoveUser(int id)
        {
            if (FindUserByID(id) == null) { return false; }
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