using System;
using System.Data;

using General.DAL.Common.Interface.UnitOfWork;

namespace General.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection = null;

        private IDbTransaction _transaction = null;

        private Guid _id = Guid.Empty;

        public UnitOfWork(IDbConnection connection)
        {
            _id = Guid.NewGuid();
            _connection = connection;
        }

        public Guid Id => _id;

        IDbConnection IUnitOfWork.Connection => _connection;

        IDbTransaction IUnitOfWork.Transaction => _transaction;

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}