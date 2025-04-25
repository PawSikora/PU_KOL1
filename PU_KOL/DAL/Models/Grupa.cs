using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Grupa
    {
        public int ID { get; set; }
        public string Nazwa { get; set; }

        public int? ParentID { get; set; }
        public Grupa Parent { get; set; }
        public ICollection<Grupa> Children { get; set; }

        public ICollection<Student> Studenci { get; set; }
    }
}
