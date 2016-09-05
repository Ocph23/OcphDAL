using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DContext
{
    public class DataTables<T> where T:class
    {
        private IDbContext context;
        IDataTable<T> table;
        public DataTables(IDbContext context)
        {
            this.context = context;
        
        }

        public IDataTable<T> GetDataTable()
        {
           
            if (context.Connection.GetType().Name == "MySqlContextConnection")
            {
                table= new MySqlDataTable<T>(context);
            }
            else if (context.Connection.GetType().Name == "SQLiteContextConnection")
            {
                table = new SQLiteDataTable<T>(context);
            }

            return table;
            
        }
    }
}
