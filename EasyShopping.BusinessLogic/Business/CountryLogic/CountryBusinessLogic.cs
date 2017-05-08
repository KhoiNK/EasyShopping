using Easyshopping.DataAccess.Repository.CountryRepo;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Business.CountryLogic
{
    public class CountryBusinessLogic
    {
        private CountryRepository _repo;
        public CountryBusinessLogic()
        {
            _repo = new CountryRepository();
        }
        public IList<CountryDTO> GetAll()
        {
            return _repo.GetAll().Translate<Country, CountryDTO>();
        }
        public CountryDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<Country, CountryDTO>();
        }
    }
}