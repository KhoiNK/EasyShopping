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
    public class DistrictBusinessLogic
    {
        private DistrictRepository _repo;

        public DistrictBusinessLogic()
        {
            _repo = new DistrictRepository();
        }

        public IEnumerable<DistrictDTO> GetAll()
        {
            return _repo.GetAll().Translate<District, DistrictDTO>();
        }

        public DistrictDTO GetById(int id)
        {
            return _repo.GetById(id).Translate<District, DistrictDTO>();
        }
    }
}
