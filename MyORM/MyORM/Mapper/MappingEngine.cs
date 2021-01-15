using System;
using System.Collections.Generic;
using System.Data;

namespace MyORM.Mapper
{
    public abstract class MappingEngine
    {
        public abstract void Execute<T>(DataRow record, T result, string propertyName, string columnName, string resultAlias) where T : class, new();

        private static Dictionary<MappingEngineType, MappingEngine> engines = new Dictionary<MappingEngineType, MappingEngine>();
        static MappingEngine()
        {
            engines.Add(MappingEngineType.TableField, new TableFieldEngine());
        }

        public static MappingEngine GetEngineFromName(MappingEngineType type)
        {
            if (!engines.ContainsKey(type))
            {
                return null;
            }
            return engines[type];
        }
    }
    public enum MappingEngineType
    {
        TableField
    }
}