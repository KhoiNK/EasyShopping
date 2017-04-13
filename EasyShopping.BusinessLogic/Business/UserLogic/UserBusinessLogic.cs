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

        public UserDTO GetUserByID(int id)
        {
            return _repo.FindUserByID(id).Translate<User, UserDTO>();
        }

        public bool Update(UserDTO user)
        {
            ClearCache(user.UserName);
            
            // _repo.Update(....)
            return true;
        }


        public Task<UserDTO> Register(UserDTO user)
        {
            return Task.Factory.StartNew(() =>
            {
                user.PassWord = Encryptor.MD5Hash(user.PassWord);
                user.RegDate = System.DateTime.Now;
                user.Modified_Date = System.DateTime.Now;

                User userEntity = user.Translate<UserDTO, User>();
                UserDTO newUser = _repo.AddUser(userEntity).Translate<User, UserDTO>();

                return newUser;

            });
        }

        public bool Delete(int id)
        {
            if (!_repo.RemoveUser(id))
            {
                return false;
            }
            return true;
        }

        public IList<UserDTO> GetAll()
        {
            return _repo.GetListUser().Translate<User, UserDTO>();
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