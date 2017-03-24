using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Easyshopping.DataAccess.Repository.Users;
using EasyShopping.BusinessLogic.Models;
using Easyshopping.DataAccess.Models.Entity;

namespace EasyShopping.BusinessLogic.Business
{
    public class UserBusinessLogic
    {
        private UserRepository _repo;
         
        public UserBusinessLogic()
        {
            _repo = new UserRepository();
        }


    }
}