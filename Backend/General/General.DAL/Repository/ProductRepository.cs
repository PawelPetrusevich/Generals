using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Dapper;

using General.DAL.Common.Models;

namespace General.DAL.Repository
{
    public class ProductRepository
    {
        private readonly string connectionString = $"Server=tcp:general-project.database.windows.net,1433;Initial Catalog=General;Persist Security Info=False;User ID=test_user;Password=Qw80336620310;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public async Task Add(Product product)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                var result = await con.ExecuteAsync("AddProduct", product, commandType: CommandType.StoredProcedure);
            }
        }
    }
}