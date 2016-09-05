using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QueryBuilder
{
  
    internal class InsertQuery
    {
        private EntityInfo entity;

        public InsertQuery(EntityInfo entity)
        {
            this.entity = entity;
        }

        internal string GetQuery(object t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert Into ").Append(entity.TableName).Append(" Values( ");

            foreach(PropertyInfo p in entity.DbTableProperty)
            {
                var att = entity.GetAttributDbColumn(p);
                if (att != null)
                {
                    sb.Append(CommonDAL.ConverConstant(p.GetValue(t))).Append(", ");
                }
            }
            var result = sb.ToString();
            sb.Clear();
            sb.Append(result.Substring(0, result.Length - 2));
            sb.Append(")");
            return sb.ToString();
        }



        internal string GetQuerywithParameter(object t)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert Into ").Append(entity.TableName).Append("(");

            foreach (PropertyInfo p in entity.DbTableProperty)
            {
                var att = entity.GetAttributDbColumn(p);
                if (att != null)
                {
                    sb.Append(entity.GetAttributDbColumn(p)).Append(", ");
                }
            }

            var result = sb.ToString();
            sb.Clear();
            sb.Append(result.Substring(0, result.Length - 2));
            sb.Append(") values (");
            foreach (PropertyInfo p in entity.DbTableProperty)
            {
                var att = entity.GetAttributDbColumn(p);
                if (att != null)
                {
                    sb.Append("@").Append(entity.GetAttributDbColumn(p)).Append(", ");
                }
            }
            result = sb.ToString();
            sb.Clear();
            sb.Append(result.Substring(0, result.Length - 2));
            sb.Append(")");
            return sb.ToString();
        }

        internal string GetLastID()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select ").Append(entity.GetAttributPrimaryKeyName())
               .Append(" From ").Append(entity.TableName)
                  .Append(" Order By ")
                  .Append(entity.GetAttributPrimaryKeyName())
                  .Append(" Desc Limit 1");
            return sb.ToString();
        }

        internal string GetChildInsertQuery(PropertyInfo p, object Item, EntityInfo entityChild)
        {
            string Query = string.Empty;
           

            PropertyInfo propParetn =entity.GetPropertyByPropertyName(p.Name);
            if (propParetn != null)
            {
                var foreignkeyinChild = (ForeignKeyAttribute)entityChild.GetAttributeForeignKeyByParentType(entity.GetEntityType());
                var PropertyPK = CommonDAL.ConverConstant(entity.PrimaryKeyProperty.GetValue(Item));
                 Query = string.Format("Select * From {0} where {1}={2}",
                    entityChild.TableName,
                    foreignkeyinChild.Name,
                    PropertyPK);
            }

            return Query;
        }

        internal void SetParameter(ref System.Data.IDbCommand cmd, object obj)
        {
            EntityInfo ent = new EntityInfo(obj.GetType());
            foreach (PropertyInfo p in ent.DbTableProperty)
            {
                cmd.Parameters.Add( new MySqlParameter(string.Format("@{0}", ent.GetAttributDbColumn(p)), CommonDAL.GetParameterValue(p,p.GetValue(obj))));
            }

        }

        
    }


}
