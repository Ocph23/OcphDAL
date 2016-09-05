using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;

namespace DAL.DContext
{
    public interface IDbContext
    {
        IDbConnection Connection{get;}

    }
}
