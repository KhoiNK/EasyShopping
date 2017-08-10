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
    public class RecruitmentBusinessLogic
    {
        private RecruitmentRepository _repo;
        public RecruitmentBusinessLogic()
        {
            _repo = new RecruitmentRepository();
        }

        public bool Create(RecruitmentDTO data)
        {
            var result = _repo.Create(data.Translate<RecruitmentDTO, Recruitment>());
            return result;
        }

        public bool Update(RecruitmentDTO data)
        {
            var result = _repo.Edit(data.Translate<RecruitmentDTO, Recruitment>());
            return result;
        }

        public RecruitmentDTO GetByStore(int storeId)
        {
            return _repo.GetByStore(storeId).Translate<Recruitment, RecruitmentDTO>();
        }
    }
}
