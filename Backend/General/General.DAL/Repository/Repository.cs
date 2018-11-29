using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using Dapper;
using General.DAL.Common.Attributes;
using General.DAL.Common.Enums;
using General.DAL.Common.Interface;
using General.DAL.Common.Interface.UnitOfWork;
using General.DAL.Common.Models;
using General.DAL.Extension;

namespace General.DAL.Repository
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : Entity
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly Lazy<string> TableName = new Lazy<string>(GetTableNameByAttribute);

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public abstract Dictionary<StoredProcedure, string> ProcedureDictionary { get; set; }

        public async Task<TModel> Add(TModel obj)
        {
            return await unitOfWork.Connection.QuerySingleAsync<TModel>(ProcedureDictionary[StoredProcedure.Add], obj, unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<string> Delete(TModel obj)
        {
            return await unitOfWork.Connection.QuerySingleAsync<string>(ProcedureDictionary[StoredProcedure.Delete], obj, unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<TModel>> Find(Expression<Func<TModel, bool>> filterExpression)
        {
            var sqlQuery = DynamicQuery.CreateWhereQuery(TableName.Value, filterExpression);

            var result = await unitOfWork.Connection.QueryAsync<TModel>(sqlQuery.Sql, sqlQuery.Param, unitOfWork.Transaction);

            return result.AsList();
        }

        private static string GetTableNameByAttribute()
        {
            return typeof(TModel).GetTypeInfo().GetCustomAttribute<TableNameAttribute>().TableName;
        }
    }
}