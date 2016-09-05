using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class ColumnInfo
    {

        public string ColumnName { get; set; }

        public Int32 Ordinal { get; set; }

        public Int32 ColumnSize { get; set; }

        public bool IsUnique { get; set; }

        public bool IsKey { get; set; }

        public string TableName { get; set; }

        public Type DataType { get; set; }

        public bool AllowNUll { get; set; }

        public Int32 ProviderType { get; set; }

        public bool IsAutoIncrement { get; set; }

        public string BaseCatalogName { get; set; }
    }
}
