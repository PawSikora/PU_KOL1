using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BLL_DB.Services
{
    public class HistoriaServiceDB : IHistoriaService
    {
        private readonly string _connectionString;

        public HistoriaServiceDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public Task AddHistoria(HistoriaDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HistoriaDTO>> GetHistoriaPage(int pageNumber, int pageSize)
        {
            using var conn = new SqlConnection(_connectionString);
            var result = await conn.QueryAsync<HistoriaDTO>(
                "PobierzHistorieStronnicowane",
                new { PageNumber = pageNumber, PageSize = pageSize },
                commandType: System.Data.CommandType.StoredProcedure);

            return result.ToList();
        }
    }
}
