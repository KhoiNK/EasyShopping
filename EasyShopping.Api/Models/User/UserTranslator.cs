using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.Api.Models.User
{
    public static class UserTranslator
    {
        public static UserDTO ToUserBusiness(this UserApiModel user)
        {
            if (user == null) { return null; }

            return new UserDTO
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

        public static IList<UserDTO> ToUserBusiness(this IEnumerable<UserApiModel> users)
        {
            if (users == null || !users.Any()) { return null; }

            return users.Select(e => e.ToUserBusiness()).ToList();

        }

        public static UserApiModel ToUserApi(this UserDTO user)
        {
            if (user == null) { return null; }
            return new UserApiModel
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

        public static IList<UserApiModel> ToUserApi(this IEnumerable<UserDTO> users)
        {
            if (users == null || !users.Any()) { return null; }
            return users.Select(e => e.ToUserApi()).ToList();
        }
    }
}