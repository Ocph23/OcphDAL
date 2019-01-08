using Ocph.DAL.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using System.Text;

namespace Ocph.DAL.Provider.SQLite
{
   public class SQLiteProviderHelper: IProvierHelper
    {
        public  object ConverConstant(object value)
        {
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

        public  object AnotherValue(object value)
        {
            Type t = value.GetType();
            if (t.IsEnum)
            {
                return string.Format("'{0}'", value.ToString());
            }
            else
                return value;
        }


        internal static string ErrorHandle(SQLiteException  ex)
        {
            string result = string.Empty;
            switch (ex.ErrorCode)
            {
                case 1062:
                    result = "Data Duplikat";
                    break;
                default:
                    result = ex.Message;
                    break;
            }

            return result;
        }

        public object CreateParameter(object fieldName, PropertyInfo p, object source)
        {

            return new SQLiteParameter("@" + fieldName, Helpers.GetParameterValue(p, p.GetValue(source)));
        }
    }
}
