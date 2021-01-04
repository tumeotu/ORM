using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper.MapperAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PrimaryKeyColumn : Attribute
    {
        public string ColumnName;
        private PrimaryKeyColumn(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
