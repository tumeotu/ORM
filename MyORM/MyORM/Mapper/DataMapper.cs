using MyORM.Mapper.MapperAttribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace MyORM.Mapper
{
    public class DataMapper : IDataMapper
    {
        private static Dictionary<Type, EntityMapper> entities = new Dictionary<Type, EntityMapper>();
        static DataMapper()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || !type.IsDefined(typeof(TableAttribute), false))
                {
                    continue;
                }
                entities.Add(type, new EntityMapper(type));
            }
        }
        public List<T> loadAll<T>(DataTable dataTable) where T : class, new()
        {
            List<T> result = new List<T>();
            EntityMapper entityMapper = null;
            if (!GetMapper(typeof(T), out entityMapper))
            {
                return result;
            }

            foreach (DataRow record in dataTable.Rows)
            {
                T entity = entityMapper.map<T>(record);
                if (entity != null)
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        public Dictionary<TKey, List<T>> loadDictionary<TKey, T>(DataTable dataTable) where T : class, new()
        {
            Dictionary<TKey, List<T>> result = new Dictionary<TKey, List<T>>();
            EntityMapper entityMapper = null;
            if (!GetMapper(typeof(T), out entityMapper))
            {
                return result;
            }

            foreach (DataRow record in dataTable.Rows)
            {
                if (record.IsNull("key"))
                {
                    continue;
                }
                T entity = entityMapper.map<T>(record);
                TKey keyValue = (TKey)record["key"];
                if (entity != null)
                {
                    if (!result.ContainsKey(keyValue))
                    {
                        List<T> list = new List<T>();
                        list.Add(entity);
                        result.Add(keyValue, list);
                    }
                    else
                    {
                        result[keyValue].Add(entity);
                    }
                }
            }
            return result;
        }

        public T loadOne<T>(DataTable dataTable) where T : class, new()
        {
            // TODO
            throw new NotImplementedException();
        }

        public string GetColumName<T>(string entityPropertyName) where T : class, new()
        {
            EntityMapper entityMapper = null;
            if (!GetMapper(typeof(T), out entityMapper))
            {
                return null;
            }
            return entityMapper.GetColumnName(entityPropertyName);
        }

        public string GetTablename<T>() where T : class, new()
        {
            EntityMapper entityMapper = null;
            if (!GetMapper(typeof(T), out entityMapper))
            {
                return null;
            }
            return entityMapper.GetTableName();
        }

        public bool IsPrimaryKey<T>(string properyName) where T : class, new()
        {
            EntityMapper entityMapper = null;
            if (GetMapper(typeof(T), out entityMapper))
            {
                return (entityMapper.IsPrimaryKey(properyName));
            }
            return false;
        }
        public bool GetMapper(Type type, out EntityMapper entityMapper)
        {
            if (!entities.TryGetValue(type, out entityMapper))
            {
                return false;
            }
            return true;
        }

        public bool IsPrimaryKeyAutoIncrement<T>(string properyName) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
