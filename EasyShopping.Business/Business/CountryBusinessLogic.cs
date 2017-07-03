using Easyshopping.Repository.Repository;
using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using System.Collections.Generic;

namespace EasyShopping.BusinessLogic.Business
{
    public class CountryBusinessLogic
    {
        private CountryRepository _repo;
        public CountryBusinessLogic()
        {
            _repo = new CountryRepository();
        }
        public IEnumerable<CountryDTO> GetAll()
        {
            return _repo.GetAll().Translate<Country, CountryDTO>();
        }
        public CountryDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<Country, CountryDTO>();
        }
    }
}