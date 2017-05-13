using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EasyShopping.BusinessLogic.Models;
using EasyShopping.BusinessLogic.CommonMethod;
using System.Threading.Tasks;
using Easyshopping.Repository.Repository.UserRepo;
using EasyShopping.Repository.Models.Entity;

namespace EasyShopping.BusinessLogic.Business
{
    public class UserBusinessLogic
    {
        private static IDictionary<string, UserDTO> Cache = new Dictionary<string, UserDTO>();

        private UserRepository _repo;

        public UserBusinessLogic()
        {
            _repo = new UserRepository();
        }

        public Task<UserDTO> Login(string username, string password)
        {
            return Task.Factory.StartNew(() =>
            {
                string hash = Encryptor.MD5Hash(password);
                string key = string.Format("{0}:{1}", username, password);
                UserDTO user = null;

                // Look in memory
                if (Cache.ContainsKey(key))
                {
                    user = Cache[key];
                }
                else
                {
                    user = _repo.FindUser(username, hash).Translate<User, UserDTO>();

                    // TODO: Must delete this key, when update user
                    Cache[key] = user;
                }

                return user;
            });

        }

        public Task<UserDTO> GetByName(string username)
        {
            return Task.Factory.StartNew(() =>
                {
                    return _repo.FindUser(username).Translate<User,UserDTO>();
                }
            );
        }

        public UserDTO GetByID(int id)
        {
            return _repo.FindByID(id).Translate<User, UserDTO>();
        }

        public bool Update(UserDTO user)
        {
            ClearCache(user.UserName);

            if (_repo.Update(user.ID, BusinessTranslators.Translate<UserDTO,User>(user)))
            {
                return true;
            }
            return false;
        }


        public Task<UserDTO> Register(UserDTO user)
        {
            return Task.Factory.StartNew(() =>
            {
                user.PassWord = Encryptor.MD5Hash(user.PassWord);
                user.RegDate = System.DateTime.Now;
                user.Modified_Date = System.DateTime.Now;

                User userEntity = user.Translate<UserDTO, User>();
                UserDTO newUser = _repo.Add(userEntity).Translate<User, UserDTO>();

                return newUser;

            });
        }

        public bool Delete(int id)
        {
            if (!_repo.Remove(id))
            {
                return false;
            }
            return true;
        }

        public IList<UserDTO> GetAll()
        {
            return _repo.GetList().Translate<User, UserDTO>();
        }


        #region Private methods

        private static void ClearCache(string username)
        {
            var keys = Cache.Keys;
            foreach (string k in keys)
            {
                // Eg: 
                // username: khoi
                // key: khoi:456
                if (k.StartsWith(username))
                {
                    Cache.Remove(k);
                }
            }
        }

        #endregion
    }
}