using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Mapper
{
    interface IDataMapper
    {
        List<T> loadAll<T>(DataTable dataTable) where T : class, new();
        T loadOne<T>(DataTable dataTable) where T : class, new();

        string GetColumName<T>(string propertyName) where T : class, new();
        string GetTablename<T>() where T : class, new();

        bool IsPrimaryKey<T>(string properyName) where T : class, new();
    }
}
