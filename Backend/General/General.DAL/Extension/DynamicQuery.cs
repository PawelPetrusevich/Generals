using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace General.DAL.Extension
{
    /// <summary>
    /// Extension for create dynamic sql query.
    /// </summary>
    internal sealed class DynamicQuery
    {
        /// <summary>
        /// Create the dynamic query.
        /// </summary>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The dynamic query from an anonymous type.
        /// </returns>
        internal static SqlQuery CreateWhereQuery<T>(string tableName, Expression<Func<T, bool>> expression)
        {
            var queryProperties = new List<QueryParameter>();
            var body = (BinaryExpression)expression.Body;
            IDictionary<string, object> expando = new ExpandoObject();
            var strBuilder = new StringBuilder();

            WalkTree(body, ExpressionType.Default, ref queryProperties);

            strBuilder.Append("SELECT * FROM ");
            strBuilder.Append(tableName);
            strBuilder.Append(" WHERE ");

            for (var i = 0; i < queryProperties.Count; i++)
            {
                var item = queryProperties[i];

                if (!string.IsNullOrWhiteSpace(item.LinkingOperator) && i > 0)
                {
                    strBuilder.Append($"{item.LinkingOperator} {item.PropertyName} {item.QueryOperator} @{item.PropertyName}");
                }
                else
                {
                    strBuilder.Append($"{item.PropertyName} {item.QueryOperator} @{item.PropertyName}");
                }

                expando[item.PropertyName] = item.PropertyValue;
            }

            return new SqlQuery(strBuilder.ToString().TrimEnd(), expando);
        }

        private static void WalkTree(BinaryExpression body, ExpressionType expressionType, ref List<QueryParameter> queryProperties)
        {
            if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
            {
                string propertyName = GetPropertyName(body);
                dynamic propertyValue = body.Right;
                string opr = GetOperator(body.NodeType);
                string link = GetOperator(expressionType);

                queryProperties.Add(new QueryParameter(link, propertyName, propertyValue.Value, opr));
            }
            else
            {
                WalkTree((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
                WalkTree((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
            }
        }

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The expression types SQL server equivalent operator.</returns>
        private static string GetOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return "AND";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Default:
                    return string.Empty;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>The property name for the property expression.</returns>
        private static string GetPropertyName(BinaryExpression body)
        {
            string propertyName = body.Left.ToString().Split(new char[] { '.' })[1];

            if (body.Left.NodeType == ExpressionType.Convert)
            {
                propertyName = propertyName.Replace(")", string.Empty);
            }

            return propertyName;
        }
    }
}