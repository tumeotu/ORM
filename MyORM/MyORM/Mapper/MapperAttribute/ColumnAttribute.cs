using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper.MapperAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        public string ColumnName;
        public ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
