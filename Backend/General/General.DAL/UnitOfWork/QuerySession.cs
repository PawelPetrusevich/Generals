using System;
using System.Data;
using System.Data.SqlClient;

namespace General.DAL.UnitOfWork
{
    public class QuerySession : IDisposable
    {
        private IDbConnection _connection = null;

        private UnitOfWork _unitOfWork = null;

        public QuerySession(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _unitOfWork = new UnitOfWork(_connection);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}