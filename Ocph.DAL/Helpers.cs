using Ocph.DAL.DbContext;
using Ocph.DAL.Provider.MySql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace Ocph.DAL
{
   internal class Helpers
    {
        public static object ConverConstant(object value)
        {
            //var a = value.GetType();
            if (value != null)
            {

                switch (value.GetType().Name)
                {
                    case "String":
                        value = string.Format("'{0}'", value);
                        break;
                    case "Boolean":
                        value = string.Format(" '{0}'", value);
                        break;
                    case "DateTime":
                        var date = (DateTime)value;
                        value = string.Format("'{0}-{1}-{2} {3}:{4}:{5}'", date.Year, date.Month, date.Day,
                            date.Hour, date.Minute, date.Second);
                        break;
                    default:
                        value = AnotherValue(value);
                        break;
                }
            }
            else
            {
                value = "'NULL'";
            }
            return value;
        }

        private static object AnotherValue(object value)
        {
            Type t = value.GetType();
            if (t.IsEnum)
            {
                return string.Format("'{0}'", value.ToString());
            }
            else
                return value;
        }


        internal static string ErrorHandle(MySql.Data.MySqlClient.MySqlException ex)
        {
            string result = string.Empty;
            switch (ex.Number)
            {
                case 1062:
                    result = "Data Duplikat";
                    break;
                default:
                  result=  ex.Message;
                    break;
            }
     
            return result;
        }

        internal static object GetParameterValue(PropertyInfo p, object obj)
        {
            object newObj = obj;

            if (p.PropertyType.IsEnum)
            {
                newObj = obj.ToString();
            }

            if (p.PropertyType.Name == "Boolean")
            {
                newObj = obj.ToString();
            }

            //if (p.PropertyType.Name == "Image" || p.PropertyType.Name == "Bitmap")
            //{
            //    Image image = (Image)obj;
            //    MemoryStream ms = new MemoryStream();
            //    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //    newObj = ms.ToArray();
            //}

            return newObj;
        }


        internal static IDataTable<T> GetDatatable<T>(IDbConnection connection) where T : class
        {

            IDataTable<T> c = null;
            var ts = connection.GetType();
            if (ts.BaseType.Name == "MySqlDbConnection")
            {
                c = new MySqlDbContext<T>(connection);
            }
            return c;
        }

    }
}
