using EasyShopping.Repository.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Easyshopping.Repository.Repository.UserRepo
{


    public class UserRepository
    {
        private const int ACTIVE = 1;
        private const int DEACTIVE = 2;
        private const int DELETED = 3;
        private EasyShoppingEntities _db = null;
        private UserManager<IdentityUser> _usermanger;
        public UserRepository()
        {
            _db = new EasyShoppingEntities();
            //_usermanger = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_db));
        }

        public User FindUser(string user_name, string password)
        {
            try
            {
                User user = _db.Users
                    .Include("Role")
                    .Include("Country")
                    .Include("UserStatu")
                    .Include("Province")
                    .Include("District")
                    .SingleOrDefault(x => ((x.UserName.Equals(user_name.Trim())) || (x.Email.Equals(user_name.Trim())))
                                                && (x.PassWord.Equals(password.Trim())));

                return user;
            }
            catch
            {
                return null;
            }
        }

        public User FindUser(string username)
        {
            try
            {
                User user = _db.Users
                    .Include("Role")
                    .Include("Country")
                    .Include("UserStatu")
                    .Include("Province")
                    .Include("District")
                    .SingleOrDefault(x => x.UserName.Equals(username.Trim()));
                return user;
            }
            catch
            {
                return null;
            }
        }

        public User FindByID(int id)
        {
            try
            {
                return _db.Users
                    .Include("Country")
                    .Include("UserStatu")
                    .Include("Province")
                    .Include("District")
                    .Include("Role")
                    .SingleOrDefault(x => x.ID == id);
            }
            catch
            {
                return null;
            }

        }

        public IEnumerable<User> FindByName(string username)
        {
            return _db.Users
                .Include("Country")
                .Include("UserStatu")
                .Include("Province")
                .Include("District")
                .Include("Role")
                .Where(x => x.UserName.Contains(username)).ToList();
        }

        public IEnumerable<User> GetList()
        {
            return _db.Users
                .Include("Country")
                .Include("UserStatu")
                .Include("Province")
                .Include("District")
                .Include("Role")
                .ToList();
        }

        public IEnumerable<User> GetList(int pageIdx, int pageSize)
        {
            int skipped = (pageIdx - 1) * pageSize;
            return _db.Users.Include("Country")
                .Include("UserStatu")
                .Include("Province")
                .Include("District")
                .Include("Role")
                .Skip(skipped)
                .Take(pageSize)
                .ToList();
        }

        public User Add(User user)
        {
            try
            {
                User newuser = _db.Users.Add(user);
                _db.SaveChanges();
                return newuser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Update(int id, User user)
        {
            User newuser = FindByID(id);
            if (newuser != null)
            {
                newuser.Address = user.Address;
                newuser.CityID = user.CityID;
                newuser.CountryID = user.CountryID;
                newuser.DistrictID = user.DistrictID;
                newuser.DOB = user.DOB;
                newuser.Email = user.Email;
                newuser.First_Name = user.First_Name;
                newuser.Img_Link = user.Img_Link;
                newuser.Last_Name = user.Last_Name;
                newuser.Modified_Date = DateTime.Now;
                newuser.PassWord = user.PassWord;
                newuser.Phone = user.Phone;
                newuser.Sex = user.Sex;
                newuser.UserName = user.UserName;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditRole(int id, int roleid)
        {
            FindByID(id).RoleID = roleid;
            _db.SaveChanges();
            return true;
        }

        public bool Remove(int id)
        {
            if (FindByID(id) == null) { return false; }
            try
            {
                FindByID(id).StatusID = DELETED;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}