using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Mapper
{
    public class EntityMapper
    {
        private string TableName;
        private Dictionary<string, string> fieldToProperty = new Dictionary<string, string>();
        private Dictionary<string, string> propertyToField = new Dictionary<string, string>();
        private Type type;

        public EntityMapper(Type type)
        {
            this.type = type;
        }

        public object map(DataRow record)
        {
            throw new NotImplementedException();
        }

        public string GetTableName()
        {
            return TableName;
        }

        internal string GetColumnName(string entityPropertyName)
        {
            throw new NotImplementedException();
        }

        internal bool IsPrimaryKey(string properyName)
        {
            throw new NotImplementedException();
        }
    }
}
