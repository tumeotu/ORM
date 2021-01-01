using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper.MapperAttribute
{
    public class ColumnAttribute : Attribute
    {
        public string ColumnName;
        private ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
