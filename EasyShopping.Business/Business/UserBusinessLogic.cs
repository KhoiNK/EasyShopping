using System.Collections.Generic;

using EasyShopping.BusinessLogic.Models;
using EasyShopping.BusinessLogic.CommonMethod;
using System.Threading.Tasks;
using Easyshopping.Repository.Repository;
using EasyShopping.Repository.Models.Entity;

namespace EasyShopping.BusinessLogic.Business
{
    public class UserBusinessLogic
    {
        private static IDictionary<string, UserDTO> Cache = new Dictionary<string, UserDTO>();

        private UserRepository _repo;

        const int STATUS_ACTIVE = 1;
        const int ROLE_MEMBER = 2;

        public UserBusinessLogic()
        {
            _repo = new UserRepository();
        }

        public Task<UserDTO> Login(string username, string password)
        {
            //var testuser = _repo.FindUser(username, Encryptor.MD5Hash(password));
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
                    return _repo.FindUser(username).Translate<User, UserDTO>();
                }
            );
        }

        public UserDTO GetByID(int id)
        {
            //System.Diagnostics.Debugger.Launch();
            UserDTO user = _repo.FindByID(id).Translate<User, UserDTO>();
            return user;
        }

        public bool Update(UserDTO user)
        {
            ClearCache(user.UserName);

            if (_repo.Update(user.ID, BusinessTranslators.Translate<UserDTO, User>(user)))
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
                user.ModifiedDate = System.DateTime.Now;
                user.StatusID = STATUS_ACTIVE;
                user.RoleID = ROLE_MEMBER;

                User userEntity = user.ToUserEntity();
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

        public IEnumerable<UserDTO> GetAll()
        {
            return _repo.GetList().Translate<User, UserDTO>();
        }

        public IEnumerable<string> GetAllUserName()
        {
            return _repo.GetAllUserName();
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