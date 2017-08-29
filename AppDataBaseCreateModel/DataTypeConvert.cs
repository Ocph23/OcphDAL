using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDataBaseCreateModel
{
    public static class DataTypeConvert
    {
        public static string TypeScript(Type type)
        {
            var result = "0";
            switch (type.Name)
            {
                case "Int32":
                    result = "number";
                    break;

                case "Int16":
                    result = "number";
                    break;

                case "Int64":
                    result = "number";
                    break;

                case "Byte[]":
                    result = "byte[]";
                    break;

                case "DateTime":
                    result = "Date";
                    break;

                case "Double":
                    result = "number";
                    break;

                case "String":
                    if (type.IsEnum)
                    {
                        result = "Enum";
                    }
                    else
                    {
                        result = "string";
                    }
                    break;


                default:
                    result = "any";
                    break;

            }

            return result;
        }

        public static string CSharp(Type type)
        {
            var result = "0";
            switch (type.Name)
            {
                case "Int32":
                    result = "int";
                    break;

                case "Int16":
                    result = "int";
                    break;

                case "Int64":
                    result = "int";
                    break;

                case "Byte[]":
                    result = "byte[]";
                    break;

                case "DateTime":
                    result = "DateTime";
                    break;

                case "Double":
                    result = "double";
                    break;

                case "String":
                    if (type.IsEnum)
                    {
                        result = "Enum";
                    }
                    else
                    {
                        result = "string";
                    }
                    break;


                default:
                    result = "0";
                    break;

            }

            return result;
        }
    }

}
