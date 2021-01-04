using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper.MapperAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        public string TableName;
        private TableAttribute(string name)
        {
            TableName = name;
        }
    }

}
