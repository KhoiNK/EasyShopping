using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Easyshopping.DataAccess.Repository.Users;
using EasyShopping.BusinessLogic.Models;
using Easyshopping.DataAccess.Models.Entity;
using EasyShopping.BusinessLogic.CommonMethod;

namespace EasyShopping.BusinessLogic.Business
{
    public class UserBusinessLogic
    {
        private UserRepository _repo;
         
        public UserBusinessLogic()
        {
            _repo = new UserRepository();
        }

        public UserDTO UserLogin(string username, string password)
        {           
            return UserTranslator.ToUserBusiness(_repo.FindUser(username,Encryptor.MD5Hash(password)));

        }

        public UserDTO Register(UserDTO user)
        {
            user.PassWord = Encryptor.MD5Hash(user.PassWord);
            user.RegDate = System.DateTime.Now;
            user.Modified_Date = System.DateTime.Now;
            return UserTranslator.ToUserBusiness(_repo.AddUser(UserTranslator.ToUserEntity(user)));
        }

        public bool DeleteUser(int id)
        {
            if(!_repo.RemoveUser(id))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<UserDTO> GetAllUser()
        {
            return UserTranslator.ToUserBusiness(_repo.GetListUser());
        }
    }
}