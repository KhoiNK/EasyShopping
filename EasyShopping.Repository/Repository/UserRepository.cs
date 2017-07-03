using EasyShopping.Repository.Models.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Easyshopping.Repository.Repository
{


    public class UserRepository
    {
        private const int ACTIVE = 1;
        private const int DEACTIVE = 2;
        private const int DELETED = 3;
        private EasyShoppingEntities _db = null;
        public UserRepository()
        {
            _db = new EasyShoppingEntities();
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
                    .Include("Ward")
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
                User user = _db.Users
                    .Include("Country")
                    .Include("UserStatu")
                    .Include("Province")
                    .Include("District")
                    .Include("Role")
                    .SingleOrDefault(x => x.ID == id);

                String countryname = user.Country.CommonName;
                String status = user.UserStatu.Name;
                String city = user.Province.Name;
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
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
                var newuser = new User{
                    Address = user.Address,
                    CityID = user.CityID,
                    CountryID = user.CountryID,
                    DistrictID = user.DistrictID,
                    DOB = user.DOB,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    ImgLink = user.ImgLink,
                    isSocialLogin = user.isSocialLogin,
                    LastName = user.LastName,
                    ModifiedDate = user.ModifiedDate,
                    PassWord = user.PassWord,
                    Phone = user.Phone,
                    RegDate = user.RegDate,
                    RoleID = user.RoleID,
                    Sex = user.Sex,
                    StatusID = user.StatusID,
                    UserName = user.UserName
                };
                //newuser = user;
                newuser = _db.Users.Add(newuser);
                _db.SaveChanges();
                return FindByID(newuser.ID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public bool Update(int id, User user)
        {
            User updateuser = FindByID(id);
            if (updateuser != null)
            {
                updateuser.Address = user.Address;
                updateuser.CityID = user.CityID;
                updateuser.CountryID = user.CountryID;
                updateuser.DistrictID = user.DistrictID;
                updateuser.DOB = user.DOB;
                updateuser.Email = user.Email;
                updateuser.FirstName = user.FirstName;
                if (!String.IsNullOrEmpty(user.ImgLink))
                {
                    updateuser.ImgLink = user.ImgLink;
                }
                updateuser.LastName = user.LastName;
                updateuser.ModifiedDate = DateTime.Now;
                updateuser.PassWord = user.PassWord;
                updateuser.Phone = user.Phone;
                updateuser.Sex = user.Sex;
                updateuser.UserName = user.UserName;
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