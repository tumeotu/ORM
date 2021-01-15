using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper.MapperAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PrimaryKeyColumn : Attribute
    {
        public string ColumnName;
        public PrimaryType ColumnType;
        public PrimaryKeyColumn(string columnName,PrimaryType type= PrimaryType.None)
        {
            this.ColumnName = columnName;
            this.ColumnType = type;
        }
    }
    public enum PrimaryType
	{
        None,
        AutoIncrement
	}
}
