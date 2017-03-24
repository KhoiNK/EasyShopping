using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public System.DateTime DOB { get; set; }
        public string Email { get; set; }
        public System.DateTime RegDate { get; set; }
        public int StatusID { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public string Img_Link { get; set; }
        public int RoleID { get; set; }
        public System.DateTime Modified_Date { get; set; }
        public int CountryID { get; set; }
    }
}