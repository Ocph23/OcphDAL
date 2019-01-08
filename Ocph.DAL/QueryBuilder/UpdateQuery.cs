using Ocph.DAL.ExpressionHandler;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Ocph.DAL.QueryBuilder
{
    public class UpdateQuery
    {
        private EntityInfo entityInfo;
      
        private object source;
        private IDbCommand command;
        public UpdateQuery(EntityInfo entity)
        {
            // TODO: Complete member initialization
            this.entityInfo = entity;
        }

        public UpdateQuery(EntityInfo entity, IDbCommand cmd)
        {
            command = cmd;
            this.entityInfo = entity;
        }

        public UpdateQuery(EntityInfo Entity, Expression fieldUpdate, Expression where, object source)
        {
            // TODO: Complete member initialization
            this.entityInfo = Entity;
            this.source = source;
        }

        public string GetQueryWithParameter(Expression fieldUpdate, Expression where, object source)
        {
            StringBuilder sb = new StringBuilder();

            var translator = new UpdateTranslator(ref command);
            sb.Append(translator.Translate(fieldUpdate, source));
            sb.Append(" where ").Append(new WhereTranslator().Translate(where));
            return sb.ToString();
        }

        internal void SetParameters(IDbCommand cmd)
        {
            cmd.Parameters.Clear();

            foreach (var param in command.Parameters)
            {
                cmd.Parameters.Add(param);
            }
        }





    }
}
