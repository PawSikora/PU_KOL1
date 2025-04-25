using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOModels
{
    public class GrupaFullNameDTO
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }
        public int? ParentID { get; set; }
        public string FullPath { get; set; }
    }
}
