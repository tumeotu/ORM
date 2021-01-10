using System;
using System.Collections.Generic;
using System.Data;

namespace MyORM.Mapper
{
    class PropertyMappingRule
    {
        private string propertyName;
        private HashSet<string> fieldsName;
        private MappingEngine mapping;

        public bool CanMap<T>(DataRow record) where T : class, new()
        {
            // TODO: check DataRow
            return true;
        }

        public void ExecuteMap<T>(DataRow record, T result, string resultAlias) where T : class, new()
        {
            this.mapping.Execute(record, result, propertyName, fieldsName, resultAlias);
        }
    }
}