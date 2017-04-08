using System;

namespace EasyShopping.BusinessLogic.Models
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }
        public int StatusID { get; set; }
        public string Phone { get; set; }
        public bool Sex { get; set; }
        public int CityID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public string Img_Link { get; set; }
        public int RoleID { get; set; }
        public DateTime Modified_Date { get; set; }
        public int CountryID { get; set; }
        public bool isSocialLogin { get; set; }
    }
}