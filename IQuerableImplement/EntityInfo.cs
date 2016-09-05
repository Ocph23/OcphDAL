using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EntityInfo
    {
        private Type type;

        public PropertyInfo[] Properties { get { return type.GetProperties(); } }
        public PropertyInfo PrimaryKeyProperty=null;
        public List<PropertyInfo> ForeignKeyProperty = new List<PropertyInfo>();
        public List<PropertyInfo> DbTableProperty = new List<PropertyInfo>();

        public EntityInfo(Type typeOfEntity)
        {
            this.type = typeOfEntity;
            this.SetAttribut();
        }

        private void SetAttribut()
        {
            foreach (PropertyInfo p in Properties)
            {
               var pk= p.GetCustomAttribute(typeof(PrimaryKeyAttribute));

                if(pk!=null)
                {
                    PrimaryKeyProperty =p;
                };

                var fk = p.GetCustomAttribute(typeof(ForeignKeyAttribute));
                if (fk != null)
                    this.ForeignKeyProperty.Add(p);


                var dbtable = p.GetCustomAttribute(typeof(DbColumnAttribute));
                if (dbtable != null)
                    this.DbTableProperty.Add(p);

            }
        }

        public string TableName
        {
            get
            {
                TableNameAttribute att = (TableNameAttribute)type.GetCustomAttributes(typeof(TableNameAttribute), true)[0];
               return att.Name;

            }
        }


        public string GetAttributPrimaryKeyName()
        {
            PrimaryKeyAttribute pk = (PrimaryKeyAttribute)PrimaryKeyProperty.GetCustomAttribute(typeof(PrimaryKeyAttribute));
           return pk.Name;
        }


        public string GetAttributForeignKeyName(int index)
        {
            var prop = this.ForeignKeyProperty[index];
            ForeignKeyAttribute fk = (ForeignKeyAttribute)PrimaryKeyProperty.GetCustomAttribute(typeof(ForeignKeyAttribute));
            return fk.Name;
        }

        public Type GetEntityType()
        {
            return type;
        }

        internal object GetAttributDbColumn(PropertyInfo property)
        {
            DbColumnAttribute field = (DbColumnAttribute)property.GetCustomAttribute(typeof(DbColumnAttribute));

            if (field != null)
                return field.Name;
            else
                return null;
        }

        internal string GetCollectionAttribute(PropertyInfo property)
        {
            CollectionAttribute field = (CollectionAttribute)property.GetCustomAttribute(typeof(CollectionAttribute));
            return field.Name;
        }

        public List<PropertyInfo> GetPropertyIsCollection()
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            foreach (PropertyInfo p in Properties.Where(O=>O.PropertyType.GenericTypeArguments.Count()>0))
            {
                    list.Add(p);
            }
            return list;
        }

        internal PropertyInfo GetPropertyByPropertyName(string p)
        {
            return this.Properties.Where(O => O.Name == p).FirstOrDefault();
        }


        internal object GetAttributeForeignKeyByParentType(Type typeparent)
        {
            object result = new object();
            foreach (PropertyInfo p in ForeignKeyProperty)
            {
                var res = p.GetCustomAttribute<ForeignKeyAttribute>(false);
                if (res.TypeOfParent == typeparent)
                {
                    result = res;
                }
            }

            return result;
        }
    }

}
