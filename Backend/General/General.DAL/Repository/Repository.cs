using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Dapper;

using General.DAL.Common.Enums;
using General.DAL.Common.Interface;
using General.DAL.Common.Models;

namespace General.DAL.Repository
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : Entity
    {
        private readonly string connectionString =
            $"Server=tcp:general-project.database.windows.net,1433;Initial Catalog=General;Persist Security Info=False;User ID=test_user;Password=Qw80336620310;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public abstract Dictionary<StoredProcedure, string> ProcedureDictionary { get; set; }

        public async Task<TModel> Add(TModel obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QuerySingleAsync<TModel>(ProcedureDictionary[StoredProcedure.Add], obj, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<string> Delete(TModel obj)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return await connection.QuerySingleAsync<string>(ProcedureDictionary[StoredProcedure.Delete], obj, commandType: CommandType.StoredProcedure);
            }
        }
    }
}