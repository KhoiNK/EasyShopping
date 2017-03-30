using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using EasyShopping.BusinessLogic.Models;

namespace EasyShopping.Models
{
    public static class UserTranslator
    {
        public static UserViewModel ToUserView(this UserDTO user)
        {
            if (user == null) { return null; }

            return new UserViewModel
            {
                ID = user.ID,
                Address = user.Address,
                CityID = user.CityID,
                CountryID = user.CountryID,
                DistrictID = user.DistrictID,
                DOB = user.DOB,
                Email = user.Email,
                First_Name = user.First_Name,
                Img_Link = user.Img_Link,
                Last_Name = user.Last_Name,
                Modified_Date = user.Modified_Date,
                PassWord = user.PassWord,
                Phone = user.Phone,
                RegDate = user.RegDate,
                RoleID = user.RoleID,
                Sex = user.Sex,
                StatusID = user.StatusID,
                UserName = user.UserName,
                isSocialLogin = user.isSocialLogin
            };

        }

        public static IList<UserViewModel> ToUserView(this IEnumerable<UserDTO> users)
        {
            if (users == null || !users.Any()) { return null; }

            return users.Select(e => e.ToUserView()).ToList();

        }

        public static UserDTO ToUserBusiness(this UserViewModel user)
        {
            if (user == null) { return null; }
            return new UserDTO
            {
                Address = user.Address,
                CityID = user.CityID,
                CountryID = user.CountryID,
                DistrictID = user.DistrictID,
                DOB = user.DOB,
                Email = user.Email,
                First_Name = user.First_Name,
                Img_Link = user.Img_Link,
                Last_Name = user.Last_Name,
                Modified_Date = System.DateTime.Now,
                PassWord = user.PassWord,
                Phone = user.Phone,
                RegDate = user.RegDate,
                RoleID = user.RoleID,
                Sex = user.Sex,
                StatusID = user.StatusID,
                UserName = user.UserName,
                isSocialLogin = user.isSocialLogin
            };
        }

        public static IEnumerable<UserDTO> ToUserBusiness(this IList<UserViewModel> users)
        {
            if (users != null || !users.Any()) { return null; }
            return users.Select(e => e.ToUserBusiness()).ToList();
        }
    }
}