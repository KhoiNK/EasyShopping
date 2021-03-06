﻿using System;

namespace EasyShopping.Api.Models
{
    public class UserApiModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        //public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }
        //public int StatusID { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }
        //public int CityID { get; set; }
        public string City { get; set; }
        //public int DistrictID { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string ImgLink { get; set; }
        //public int RoleID { get; set; }
        public string Role { get; set; }
        public DateTime ModifiedDate { get; set; }
        //public int CountryID { get; set; }
        public string Country { get; set; }
        public bool isSocialLogin { get; set; }
        public string Ward { get; set; }
    }

    public class AddUserModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int DistrictID { get; set; }
        public int WardID { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string ImgLink { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
        public bool isSocialLogin { get; set; }
    }
}