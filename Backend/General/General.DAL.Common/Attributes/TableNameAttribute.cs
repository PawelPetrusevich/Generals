using System;

namespace General.DAL.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get;private set; }

        public TableNameAttribute(string tableName)
        {
            this.TableName = tableName;
        }
    }
}