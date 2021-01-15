using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper.MapperAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        public string TableName;
        public TableAttribute(string name)
        {
            TableName = name;
        }
    }

}
