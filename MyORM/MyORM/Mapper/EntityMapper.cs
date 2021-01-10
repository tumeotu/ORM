using MyORM.Mapper.MapperAttribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Mapper
{
    public class EntityMapper
    {
        private string TableName;
        private Dictionary<string, string> databaseToEntity = new Dictionary<string, string>();
        private Dictionary<string, string> entityToDatabase = new Dictionary<string, string>();
        private HashSet<string> primaryKeyProperty = new HashSet<string>();
        private List<PropertyMappingRule> rules = new List<PropertyMappingRule>();
        public EntityMapper(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                // TODO: add prop - field and field - prop dictionary
                string propOfEntity = property.Name;
                string columnOfTable = null;
                if (property.IsDefined(typeof(ColumnAttribute), false))
                {

                    var attributesOfProperty = property.GetCustomAttributes(false);

                    foreach (object attribute in attributesOfProperty)
                    {
                        ColumnAttribute columnAttribute = attribute as ColumnAttribute;
                        if (columnAttribute != null)
                        {
                            columnOfTable = columnAttribute.ColumnName;
                        }
                    }
                    // TODO: add primary key
                }
                else if (property.IsDefined(typeof(PrimaryKeyColumn), false))
                {

                    var attributesOfProperty = property.GetCustomAttributes(false);

                    foreach (object attribute in attributesOfProperty)
                    {
                        PrimaryKeyColumn primaryColumnAttribute = attribute as PrimaryKeyColumn;
                        if (primaryColumnAttribute != null)
                        {
                            columnOfTable = primaryColumnAttribute.ColumnName;
                            primaryKeyProperty.Add(propOfEntity);
                        }
                    }
                }
                else
                {
                    continue;
                }
                this.databaseToEntity.Add(columnOfTable, propOfEntity);
                this.entityToDatabase.Add(propOfEntity, columnOfTable);
                // TODO: add rule
                this.rules.Add(new PropertyMappingRule(propOfEntity, columnOfTable, MappingEngine.GetEngineFromName(MappingEngineType.TableField));
            }
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
            return this.entityToDatabase[entityPropertyName];
        }

        public bool IsPrimaryKey(string propertyName)
        {
            return this.primaryKeyProperty.Contains(propertyName);
        }

    }
}
