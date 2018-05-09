using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Drawing;

namespace Ocph.DAL.Mapping
{
    public static class MappingProperties<T>
    {
      //  private EntityInfo entityParent;
     //   private List<ColumnInfo> ReaderSchema;

        //public MappingProperties(EntityInfo entity)
        //{
        //    this.entityParent = entity;
        //}


        public static List<T> MappingTable(IDataReader dr)
        {
            List<T> list = new List<T>();
            
            while (dr.Read())
            {
                T obj = default(T);
                obj = Activator.CreateInstance<T>();
                list.Add(WriteColumnMappings(obj, dr));
            }
            return list;
        }

        private static T WriteColumnMappings(T obj, IDataReader dr)
        {
            var entity = new EntityInfo(typeof(T));
            var ReaderSchema = MappingCommon.ReadColumnInfo(dr.GetSchemaTable());
            foreach (var property in entity.Properties)
            {
              //  var columnMapping = entityParent.GetAttributDbColumn(property);
               if(!property.SetMethod.IsVirtual)
                {
                    var field = ReaderSchema.Where(O => O.ColumnName.ToString().ToUpper() == property.Name.ToString().ToUpper()).FirstOrDefault();
                    if (field != null)
                    {
                        var value = dr.GetValue(field.Ordinal);
                        if (value is DBNull)
                        {

                        }
                        else
                        {
                            try
                            {
                                property.SetValue(obj, GetValue(property, value));
                            }
                            catch (Exception ex)
                            {

                                throw new SystemException(ex.Message);
                            }

                        }
                    }
                }
            }

            return (T)obj;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }


       

        private static object GetValue(PropertyInfo property, object p)
        {
            object result=new object();

            if (property.PropertyType.IsEnum)
            {
               var ass= Enum.Parse(property.PropertyType, (string)p, true);

                result = Enum.ToObject(property.PropertyType, Convert.ToUInt64(ass));//
            }else
                result =  ConverValue(property, p);

            return result;
        }

       
        private static void SetListValue<T>(T t, List<T> ListParet)
        {
            ListParet.Add(t); ;
        }

        private static object ConverValue(System.Reflection.PropertyInfo p, object obj)
        {
            var propertyType = p.PropertyType;

            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            obj = Convert.ChangeType(obj, targetType);





            switch (propertyType.Name)
            { 
                case  "Boolean":
                    obj = Convert.ToBoolean(obj);
                    break;

                case "Image":
                    obj = GetImage(obj);
                    break;
                default:
                    obj = Convert.ChangeType(obj, targetType);
                    break;
            }

            return obj;
        }

        private static object GetImage(object obj)
        {

            if (obj is DBNull)
            {
                obj = null;
            }
           
            return obj;
        }

      

    }
}
