using System.Text;
using System.Linq.Expressions;
using Ocph.DAL.ExpressionHandler;

namespace Ocph.DAL.QueryBuilder
{
    internal class DeleteQuery
    {
        private EntityInfo entityInfo;
        public DeleteQuery(EntityInfo entity)
        {
            // TODO: Complete member initialization
            this.entityInfo = entity;
        }

        public string  GetQuery(Expression expression)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Delete From ").Append(entityInfo.TableName).Append(" Where ");
            string whereHanlder = new WhereTranslator().Translate(expression);
            sb.Append(whereHanlder);
            return sb.ToString();
        }
    }
}
