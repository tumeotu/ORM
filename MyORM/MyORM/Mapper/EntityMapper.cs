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
        private HashSet<KeyValuePair<string, string>> primaryKeys = new HashSet<KeyValuePair<string, string>>();
        private List<PropertyMappingRule> rules = new List<PropertyMappingRule>();
        public EntityMapper(Type type)
        {
            // TODO: add prop - field and field - prop dictionary
            // TODO: add primary key
            // TODO: add rule
        }

        public T map<T>(DataRow record, string resultAlias = null) where T : class, new()
        {
            T result = new T();
            if (resultAlias == null)
            {
                resultAlias = this.TableName;
            }
            foreach (var rule in rules)
            {
                if (rule.CanMap<T>(record))
                {
                    rule.ExecuteMap<T>(record, result, resultAlias);
                }
            }
            return result;
        }

        public string GetTableName()
        {
            return TableName;
        }

        public string GetColumnName(string entityPropertyName)
        {
            return this.propertyToField[entityPropertyName];
        }

        public bool IsPrimaryKey(string propertyName)
        {
            return this.primaryKeys.Contains(new KeyValuePair<string, string>(propertyName, this.propertyToField[propertyName]));
        }

    }
}
