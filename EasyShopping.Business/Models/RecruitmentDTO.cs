using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShopping.BusinessLogic.Models
{
    public class RecruitmentDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public Nullable<int> StoreId { get; set; }
    }
}
