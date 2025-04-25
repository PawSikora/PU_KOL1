using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudenci();
        Task<StudentDTO> GetStudentById(int id);
        Task<StudentDTO> CreateStudent(StudentDTO dto);
        Task<StudentDTO> UpdateStudent(int id, StudentDTO dto);
        Task<bool> DeleteStudent(int id);
    }
}
