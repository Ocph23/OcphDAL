using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ExpressionHandler
{
    public class UpdateTranslator : ExpressionVisitor
    {
        public UpdateTranslator(ref IDbCommand command)
        {
            this.command = command;
        }


        private StringBuilder sb = new StringBuilder();
        private object source;
        private string _updateQuery;
        private IDbCommand command;

        public string UpdateQuery
        {
            get { return _updateQuery; }
            set { _updateQuery = value; }
        }


        internal string Translate(Expression fieldUpdate, object source)
        {
            EntityInfo entity = new EntityInfo(source.GetType());
            this.sb = new StringBuilder();
            this.source = source;
            sb.Append("Update ").Append(entity.TableName).Append(" Set ");
            this.Visit(fieldUpdate);
            var result = sb.ToString();
            _updateQuery = string.Format(result.Substring(0, result.Length - 2));
            return _updateQuery;
        }


        protected override Expression VisitMember(MemberExpression node)
        {
            Type type = node.Member.ReflectedType;
            EntityInfo entity = new EntityInfo(type);
            PropertyInfo p = entity.GetPropertyByPropertyName(node.Member.Name);
            var fieldName = entity.GetAttributDbColumn(p);
            sb.Append(fieldName).Append("=").Append("@" + fieldName).Append(", ");
            command.Parameters.Add(new MySqlParameter("@" + fieldName, CommonDAL.GetParameterValue(p, p.GetValue(source))));

            return base.VisitMember(node);
        }
    }
}
