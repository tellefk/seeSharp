using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DotnetAPI
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T>(string sql)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await dbConnection.QueryAsync<T>(sql);
            }
        }

        public async Task<T> LoadDataSingle<T>(string sql)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await dbConnection.QuerySingleAsync<T>(sql);
            }
        }

        public async Task<T> LoadDataPostgresSingle<T>(string sql)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_config.GetConnectionString("PostgresConnection")))
            {
                return await dbConnection.QuerySingleAsync<T>(sql);
            }
        }

        public async Task<IEnumerable<T>> LoadDataPostgres<T>(string sql)
        {
            using (IDbConnection dbConnection = new NpgsqlConnection(_config.GetConnectionString("PostgresConnection")))
            {
                return await dbConnection.QueryAsync<T>(sql);
            }
        }


        public async Task<bool> ExecuteSql(string sql)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await dbConnection.ExecuteAsync(sql) > 0;
            }
        }

        public async Task<int> ExecuteSqlWithRowCount(string sql)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return await dbConnection.ExecuteAsync(sql);
            }
        }
    }
}
