using Ocph.DAL.Provider.MySql;
using Ocph.DAL.Repository;
using System;

namespace AppDataBaseCreateModel
{
    internal class OcphDbContext : MySqlDbConnection
    {
        public OcphDbContext(string constr) : base(constr)
        {
            ConnectionString = constr;
        }
        IRepository<absen> Absen { get { return new Repository<absen>(this); } }
    }


    public class absen
    {
        public int Id { get; set; }
        public string IdMahasiswa{get;set;}
    }


    public class anggota
    {
        public virtual string Alamat { get; set; }
        public int Idmahasiswa { get; set; }
        public string Nama { get; set; }
    }


}