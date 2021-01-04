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
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || !type.IsDefined(typeof(TableAttribute), false))
                {
                    continue;
                }
                entities.Add(type, new EntityMapper(type));
            }
        }
        public List<T> loadAll<T>(DataTable dataTable)
        {
            List<T> result = new List<T>();
            EntityMapper entityMapper = null;
            if (GetMapper(typeof(T), entityMapper))
            {
                return result;
            }

            foreach (DataRow record in dataTable.Rows)
            {
                T entity = (T)entityMapper.map(record);
                if (entity != null)
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        public T loadOne<T>(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public bool GetMapper(Type type, EntityMapper entityMapper)
        {
            if (!entities.TryGetValue(type, out entityMapper))
            {
                return false;
            }
            return true;
        }

        public string GetColumName<T>(string entityPropertyName)
        {
            EntityMapper entityMapper = null;
            if (!GetMapper(typeof(T), entityMapper))
            {
                return null;
            }
            return entityMapper.GetColumnName(entityPropertyName);
        }

        public string GetTablename<T>()
        {
            EntityMapper entityMapper = null;
            if (!GetMapper(typeof(T), entityMapper))
            {
                return null;
            }
            return entityMapper.GetTableName();
        }

        public bool IsPrimaryKey<T>(string properyName)
        {
            EntityMapper entityMapper = null;
            if (GetMapper(typeof(T), entityMapper))
            {
                return (entityMapper.IsPrimaryKey(properyName));
            }
            return false;
        }
    }
}
