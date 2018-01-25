using DAL.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDataBaseCreateModel
{
    public partial class Form1 : Form
    {
        DAL.MyConfiguration config = new DAL.MyConfiguration();
        private StringBuilder sb;
        private StringBuilder sbview;
        private string constr;
        private string commandText;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            SettingDatabase = new DatabaseSetting
            {
                Server = config.Server,
                Database = config.Database,
                UserName = config.User,
                Password = config.Password,
                Port = config.Port.ToString()
            };

            this.FolderPath.Text = config.Path;
            this.Poco.Checked = true;
            this.Modern.Checked = true;
            this.Header.Text = @"using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;";

            
            this.constr = string.Format("Server={0};database={1};UID={2};password={3};Port={4};CharSet=utf8;Persist Security Info=True",
               SettingDatabase.Server, SettingDatabase.Database, SettingDatabase.UserName, SettingDatabase.Password, SettingDatabase.Port);
            this.commandText= string.Format(@"SELECT TABLE_SCHEMA, DATA_TYPE, COLUMN_TYPE, TABLE_NAME 
                                    FROM INFORMATION_SCHEMA.COLUMNS where
                                        `columns`.`TABLE_Schema`='{0}' group by table_name", SettingDatabase.Database);


        }


        private List<MySchemaInfor> GetTable(IDataReader dr)
        {
           var list =new List<MySchemaInfor>();

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

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Setting();
            form.ShowDialog();
            this.SettingDatabase = form.Model;
        }


        public void CreateFile(StringBuilder sb, MySchemaInfor itemtable,string extention)
        {
            string path = string.Format("{0}\\{1}.{2}", FolderPath.Text, itemtable.TableName,extention);

            try
            {

                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(sb.ToString());
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public DatabaseSetting SettingDatabase { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            Body.Text = string.Empty;
            this.sb = new StringBuilder();
            this.sbview = new StringBuilder();


            if (cSharp.Checked)
            {
                this.Body.Text = CreatePoco();
            }else if (typeScript.Checked)
            {
                this.Body.Text = CreateTypeScripModel();

            }else if(php.Checked)
            {
                this.Body.Text = CreatePhpModel();
            }
               
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var dr = dialog.ShowDialog();
            FolderPath.Text = dialog.SelectedPath;
            config.UpdateKey("Path", this.FolderPath.Text);
        }


        //metode
        public string CreatePoco()
        {
           
            using (var db = new ContextTest(constr))
            {
                try
                {
                    var cmd = db.Connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = this.commandText;
                    IDataReader dr = cmd.ExecuteReader();
                    var s = dr.GetSchemaTable();
                    List<MySchemaInfor> list = this.GetTable(dr);
                    var ns = "";
                    if (!string.IsNullOrEmpty(namespacess.Text))
                    {
                        ns = namespacess.Text;
                    }
                    else
                    {
                        ns = SettingDatabase.Database;
                    }


                    //  sb.Append(Header.Text);

                    List<string> listprivate = new List<string>();

                    foreach (var itemtable in list)
                    {

                        sb.Append(string.Format("\n \n namespace {0} \n", ns));
                        sb.Append("{ \n");

                        if (OcphDal.Checked)
                        {
                            sb.Append(string.Format("     [TableName({0}{1}{2})] \n", '"', itemtable.TableName, '"'));

                        }



                        sb.Append(string.Format("     public class {0}", itemtable.TableName));

                        if (Inpc.Checked)
                        {
                            sb.Append(":BaseNotifyProperty");
                        }

                        sb.Append("  \n   {\n");

                        cmd.CommandText = string.Format("Select * From {0}.{1} limit 1", itemtable.Database, itemtable.TableName);
                        dr = cmd.ExecuteReader();
                        List<ColumnInfo> ReaderSchema = MappingCommaon.ReadColumnInfo(dr.GetSchemaTable());

                        foreach (var item in ReaderSchema)
                        {
                            if (OcphDal.Checked && item.IsKey)
                            {
                                sb.Append(string.Format("          [PrimaryKey({0}{1}{2})] \n", '"', item.ColumnName, '"'));

                            }
                            if (OcphDal.Checked)
                            {
                                sb.Append(string.Format("          [DbColumn({0}{1}{2})] \n", '"', item.ColumnName, '"'));

                            }

                            if (Modern.Checked)
                            {
                                sb.Append(string.Format("          public {0} {1} ", DataTypeConvert.CSharp(item.DataType), item.ColumnName));
                                sb.Append("{  get; set;} \n\n");
                            }
                            else
                            {
                                sb.Append(string.Format("          public {0} {1} ", DataTypeConvert.CSharp(item.DataType), item.ColumnName));
                                sb.Append("\n          { \n               get{return ");
                                sb.Append(string.Format("_{0};", item.ColumnName.ToLower()));
                                sb.Append("} \n               set{ \n");
                             
                                if (!Inpc.Checked)
                                {
                                    sb.Append(string.Format("                      _{0}=value;", item.ColumnName.ToLower()));
                                }
                                else
                                {
                                    sb.Append(string.Format("SetProperty(ref _{0}, value);", item.ColumnName.ToLower()));
                                }

                                sb.Append("\n                     }\n          } \n\n");
                                var p = string.Format("          private {0}  _{1};\n ", DataTypeConvert.CSharp(item.DataType), item.ColumnName.ToLower());
                                listprivate.Add(p);

                            }

                        }

                        if (Clasic.Checked)
                        {
                            foreach (var i in listprivate)
                            {
                                sb.Append(i);
                            }
                            listprivate.Clear();
                        }
                        dr.Close();








                        sb.Append("     }\n");
                        sb.Append("}\n\n\n");


                        if (aClass1File.Checked)
                        {
                            if (OcphDal.Checked)
                            {
                                sb.Insert(0, "using DAL;");
                            }
                            sb.Insert(0, Header.Text);
                            sbview.Append(sb.ToString());
                            CreateFile(sb, itemtable,"cs");
                            sb.Clear();
                        }
                    }





                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            if (aClass1File.Checked)
            {
                return sbview.ToString();
            }
            else
                return sb.ToString();

        }


        public string CreateTypeScripModel()
        {

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
                if (!string.IsNullOrEmpty(namespacess.Text))
                {
                    ns = namespacess.Text;
                }
                else
                {
                    ns = SettingDatabase.Database;
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
                    if (aClass1File.Checked)
                    {
                        sbview.Append(sb.ToString());
                        CreateFile(sb, itemtable,"ts");
                        sb.Clear();
                    }


                }



            }
            if (aClass1File.Checked)
            {
                return sbview.ToString();
            }
            else
                return sb.ToString();


        }

        public string CreatePhpModel()
        {

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
                if (!string.IsNullOrEmpty(namespacess.Text))
                {
                    ns = namespacess.Text;
                }
                else
                {
                    ns = SettingDatabase.Database;
                }

                List<string> listprivate = new List<string>();

                foreach (var itemtable in list)
                {

                    sb.Append(string.Format("<?php\n \n namespace {0} \n", ns));
                    sb.Append("{ \n");


                    sb.Append(string.Format("     class {0}", itemtable.TableName));

                    sb.Append("  \n   {\n");

                    cmd.CommandText = string.Format("Select * From {0}.{1} limit 1", itemtable.Database, itemtable.TableName);
                    dr = cmd.ExecuteReader();
                    List<ColumnInfo> ReaderSchema = MappingCommaon.ReadColumnInfo(dr.GetSchemaTable());

                    foreach (var item in ReaderSchema)
                    {

                        sb.Append(string.Format("      ${0};\n ", item.ColumnName));

                        //var p = string.Format("          private {0}  _{1};\n ", this.DataTypeConvert(item.DataType), item.ColumnName.ToLower());
                        //listprivate.Add(p);


                    }

                    dr.Close();
                    sb.Append("     }\n");
                    sb.Append("}\n\n\n ?>");
                    if (aClass1File.Checked)
                    {
                        sbview.Append(sb.ToString());
                        CreateFile(sb, itemtable, "php");
                        sb.Clear();
                    }


                }



            }
            if (aClass1File.Checked)
            {
                return sbview.ToString();
            }
            else
                return sb.ToString();


        }




    }



    public class MySchemaInfor
    {
        public string Database { get; set; }
        public string DataType { get; set; }
        public string TableName { get; set; }
        public string ColumnType { get; set; }
    }

}







