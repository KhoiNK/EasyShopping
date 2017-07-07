using EasyShopping.BusinessLogic.Models;
using EasyShopping.Repository.Models.Entity;
using EasyShopping.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.BusinessLogic.Business
{
    public class CityBusinessLogic
    {
        CityRepository _repo;
        public CityBusinessLogic()
        {
            _repo = new CityRepository();
        }

        public IEnumerable<ProvinceDTO> GetAll()
        {
            return _repo.GetAll().Translate<Province, ProvinceDTO>();
        }

        public ProvinceDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<Province, ProvinceDTO>();
        }
    }
}
