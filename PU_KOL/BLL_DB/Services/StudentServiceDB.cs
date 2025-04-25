using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BLL_DB.Services
{
    public class StudentServiceDB : IStudentService
    {

        private readonly string _connectionString;

        public StudentServiceDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public async Task<StudentDTO> CreateStudent(StudentDTO dto)
        {
            using var conn = new SqlConnection(_connectionString);

            var param = new
            {
                Imie = dto.Imie,
                Nazwisko = dto.Nazwisko,
                IDGrupy = dto.IDGrupy
            };

            var result = await conn.QueryAsync<int>(
                "DodajStudenta",
                param,
                commandType: System.Data.CommandType.StoredProcedure);

            var newId = result.FirstOrDefault();

            return new StudentDTO()
            {
                ID = newId,
                Imie = dto.Imie,
                Nazwisko = dto.Nazwisko,
                IDGrupy = dto.IDGrupy
            };
        }

        public Task<bool> DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentDTO>> GetAllStudenci()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> GetStudentById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDTO> UpdateStudent(int id, StudentDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
