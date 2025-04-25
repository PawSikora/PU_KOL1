using System;
using System.Collections.Generic;
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
    public class GrupaServiceDB : IGrupaService
    {
        private readonly string _connectionString;

        public GrupaServiceDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public async Task<List<GrupaFullNameDTO>> GetAllGrupy()
        {
            using var conn = new SqlConnection(_connectionString);

            var sql = @"
            WITH GroupHierarchy AS (
                SELECT 
                    ID,
                    Nazwa,
                    ParentID,
                    CAST(Nazwa AS NVARCHAR(MAX)) AS FullPath
                FROM Grupa
                WHERE ParentID IS NULL

                UNION ALL

                SELECT 
                    g.ID,
                    g.Nazwa,
                    g.ParentID,
                    CAST(gh.FullPath + ' / ' + g.Nazwa AS NVARCHAR(MAX)) AS FullPath
                FROM Grupa g
                INNER JOIN GroupHierarchy gh ON g.ParentID = gh.ID
            )
            SELECT ID, Nazwa, ParentID, FullPath FROM GroupHierarchy
            ORDER BY FullPath
        ";

            var result = await conn.QueryAsync<GrupaFullNameDTO>(sql);
            return result.ToList();
        }
    }
}
