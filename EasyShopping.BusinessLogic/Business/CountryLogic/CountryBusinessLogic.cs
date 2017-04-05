using Easyshopping.DataAccess.Repository.CountryRepo;
using EasyShopping.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Business.CountryLogic
{
    public class CountryBusinessLogic
    {
        private CountryRepository _repo = null;
        public CountryBusinessLogic()
        {
            _repo = new CountryRepository();
        }
        public IList<CountryDTO> GetAll()
        {
            return _repo.GetAll().ToCountryBusiness();
        }
    }
}