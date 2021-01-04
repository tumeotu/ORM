using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Mapper
{
    interface IDataMapper
    {
        List<T> loadAll<T>(DataTable dataTable);
        T loadOne<T>(DataTable dataTable);

        string GetColumName<T>(string propertyName);
        string GetTablename<T>();

        bool IsPrimaryKey<T>(string properyName);
    }
}
