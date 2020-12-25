using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Mapper
{
    class DBMapper
    {
        static Dictionary<string, string> mapping = new Dictionary<string, string>() {
            { "name","nameColumn" },
            { "DTB","DTBColumn" },
            { "ID","IDColumn" },
        };
        internal static string getColumName<T>(string name)
        {
            return mapping[name];
        }

        internal static string getValue<T>(T obj)
        {
            return "";
        }

        internal static string getColumName<T>()
        {
            return "";
        }

        internal static string getTablename<T>()
        {
            return typeof(T).Name;
        }
    }
}
