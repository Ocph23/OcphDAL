using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using AppDataBaseCreateModel;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DAL.Mapping;

namespace DALUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private string namespacess;
        private StringBuilder sb = new StringBuilder();
        private string result;

        [TestMethod]
        public void TestMethod1()
        {
            var constr = "Server=localhost;database=dbstokobat;UID=root;password=;Port=3306;CharSet=utf8;Persist Security Info=True";

            using (var ctx = new ContextTest(constr))
            {
                var cmd = ctx.Connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = string.Format(@"SELECT TABLE_SCHEMA, DATA_TYPE, COLUMN_TYPE, TABLE_NAME 
                                    FROM INFORMATION_SCHEMA.COLUMNS where
                                        `columns`.`TABLE_Schema`='{0}' group by table_name", "dbstokobat");

                IDataReader dr = cmd.ExecuteReader();
                var s = dr.GetSchemaTable();

                List<MySchemaInfor> list = this.GetTable(dr);
                var ns = "";
                if (!string.IsNullOrEmpty(namespacess))
                {
                    ns = namespacess;
                }
                else
                {
                    ns = "dbstokobat";
                }
                List<string> listprivate = new List<string>();

                foreach (var itemtable in list)
                {

                    sb.Append(string.Format("\n \n namespace {0} \n", ns));
                    sb.Append("{ \n");


                    sb.Append(string.Format("    export class {0}", itemtable.TableName));

                    sb.Append("  \n   {\n");

                    cmd.CommandText = string.Format("Select * From {0}.{1} limit 1", itemtable.Database, itemtable.TableName);
                    dr = cmd.ExecuteReader();
                    List<ColumnInfo> ReaderSchema = MappingCommaon.ReadColumnInfo(dr.GetSchemaTable());

                    foreach (var item in ReaderSchema)
                    {

                        sb.Append(string.Format("public {0}: {1};\n ", item.ColumnName, DataTypeConvert.TypeScript(item.DataType)));
                       
                        //var p = string.Format("          private {0}  _{1};\n ", this.DataTypeConvert(item.DataType), item.ColumnName.ToLower());
                        //listprivate.Add(p);


                    }

                 
                    dr.Close();








                    sb.Append("     }\n");
                    sb.Append("}\n\n\n");

                    result = sb.ToString();
                    var a = 1;
                    sb.Clear();
                }



            }
          
            
        }
    
        private List<MySchemaInfor> GetTable(IDataReader dr)
        {
            var list = new List<MySchemaInfor>();

            while (dr.Read())
            {

                MySchemaInfor info = new MySchemaInfor();
                info.Database = dr[0].ToString();
                info.DataType = dr[1].ToString();
                info.ColumnType = dr[2].ToString();
                info.TableName = dr[3].ToString();
                list.Add(info);
            }
            dr.Close();
            return list;

        }
    }
}
