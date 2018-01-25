using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using DAL.DContext;
using System.Data;

namespace DAL
{
    public class CommonDAL
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
                        var date =(DateTime)value;
                        value = string.Format("'{0}-{1}-{2} {3}:{4}:{5}'",date.Year,date.Month,date.Day,
                            date.Hour,date.Minute,date.Second);
                        break;
                    default:
                        value=AnotherValue(value);
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
            string result = ex.Message;
            if(ex.Number == 1062)
            {
               result="Data Sudah Ada";
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
            if (p.PropertyType.Name == "Image" || p.PropertyType.Name == "Bitmap")
            {
                Image image = (Image)obj;
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                newObj = ms.ToArray();
            }

            return newObj;
        }


        internal static DContext.IDataTable<T> GetDatatable<T>(DContext.IDbContext dataContext) where T:class
        {
          
             IDataTable<T> c=null;
            var Conn = dataContext.Connection;
            var ts = Conn.GetType();
            if(ts.Name== "MySqlDbContext")
            {
                c= new MySqlDataTable<T>(dataContext);
            }else if(ts.Name== "SQLiteContextConnection")
            {
                c = new SQLiteDataTable<T>(dataContext);
            }
            return c;
        }


    }
}