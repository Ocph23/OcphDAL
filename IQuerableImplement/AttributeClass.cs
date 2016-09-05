using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   

    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttribute : Attribute
    {
        private string _name;

        public string Name { get { return _name; } private set { _name = value; } }


        public DbColumnAttribute(string name)
        {
            this.Name = name;
        }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public TableNameAttribute(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; private set; }
    }



    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute(string Name)
        {
            this.Name = Name;
        }

        public string Name { get; set; }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public ForeignKeyAttribute(string Name, Type TypeOfParent)
        {
            this.Name = Name;
            this.TypeOfParent = TypeOfParent;
        }

        public string Name { get; set; }

        public Type TypeOfParent { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CollectionAttribute : Attribute
    {
        public CollectionAttribute(string Name, Type TypeOfMember)
        {
            this.Name = Name;
            this.TypeOfMember = TypeOfMember;
        }

        public string Name { get; set; }

        public Type TypeOfMember { get; set; }
    }


}
