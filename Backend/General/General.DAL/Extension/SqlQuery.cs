using System;

namespace General.DAL.Extension
{
    public class SqlQuery
    {
        private readonly Tuple<string, object> _result;

        public SqlQuery(string sql, dynamic param)
        {
            _result = new Tuple<string, object>(sql, param);
        }

        public string Sql => _result.Item1;

        public object Param => _result.Item2;
    }
}