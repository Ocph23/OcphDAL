using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocph.DAL.Mapping.MySql
{
    public class MappingCommon
    {
        public static List<ColumnInfo> ReadColumnInfo(DataTable TableSchema)
        {
            List<ColumnInfo> list = new List<ColumnInfo>();
            foreach (DataRow row in TableSchema.Rows)
            {
                ColumnInfo mr = new ColumnInfo();
                object[] info = row.ItemArray;
                mr.ColumnName = info[0].ToString();
                mr.Ordinal = ((Int32)info[1]-1);
                mr.ColumnSize =(Int32) info[2];
                mr.IsUnique = (bool)info[5];
                mr.IsKey = (bool)info[6];
                mr.BaseCatalogName = info[9].ToString();
                mr.TableName = info[10].ToString();
                mr.DataType = (Type)info[11];
                mr.AllowNUll = (bool)info[12];
                mr.ProviderType =(Int32) info[13];
                mr.IsAutoIncrement =(bool) info[17];
                list.Add(mr);
            }
            return list;
        }

    }
}

 