using DAL.ExpressionHandler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QueryBuilder
{
    public class UpdateQuery
    {
        private EntityInfo entityInfo;
      
        private object source;
        private IDbCommand command = new MySqlCommand();
        public UpdateQuery(EntityInfo entity)
        {
            // TODO: Complete member initialization
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
