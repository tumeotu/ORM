using MyORM.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MyORM.SQLBuilder
{
    class SqlString<T> : SqlBuilder<T>
    {
        public String sql { get; set; }

        public void save(object ob)
        {
            string columnNameString = "";
            string valueString = "";
            foreach (PropertyInfo prop in ob.GetType().GetProperties())
            {
                string porpName = prop.Name;
                var porpValue = prop.GetValue(ob, null);
                if (porpValue != null)
                {
                    columnNameString += (DBMapper.getColumName<T>(porpName) + ",");
                    if (prop.PropertyType == typeof(string))
                    {
                        valueString += ("'" + porpValue + "'" + ",");
                    }
                    else
                    {
                        valueString += (porpValue.ToString() + ",");
                    }
                }
            }

            this.sql = "Inser Into " + DBMapper.getTablename<T>() + "(" + columnNameString.Remove(columnNameString.Length - 1) + ") Values (" + valueString.Remove(valueString.Length - 1) + ")";

        }

        public SqlBuilder<T> AND(Expression<Func<T, bool>> clause)
        {
            throw new NotImplementedException();
        }

        public SqlBuilder<T> SelectAll()
        {
            this.sql = "SELECT * FROM " + DBMapper.getTablename<T>();
            return this;
        }

        public SqlBuilder<T> Update(T obj)
        {
            this.sql = "UPDATE " +DBMapper.getTablename<T>() + " SET ";// lấy danh sách các thuộc tính của đối tượng

            string setString = "";
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                string porpName = prop.Name;
                var porpValue = prop.GetValue(obj, null);
                if (porpValue != null)
                {
                    setString += DBMapper.getColumName<T>(porpName) + "=";
                    if (prop.PropertyType == typeof(string))
                    {
                        setString += ("'" + porpValue + "'" + ",");
                    }
                    else
                    {
                        setString += (porpValue.ToString() + ",");
                    }
                }
                setString.Remove(setString.Length - 1);
            }
            this.sql += setString;
            return this;
        }

        public SqlBuilder<T> Where(Expression<Func<T, bool>> clause)
        {
            this.sql += " WHERE " + parseClause(clause.Body.ToString());
            return this;
        }
        private static string parseClause(String clause)
        {
            char[] separators = new char[] { '(', ')', ' ', };
            string[] subs = clause.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string left = (subs[0].Split('.', (char)StringSplitOptions.RemoveEmptyEntries))[1];
            string midle = subs[1];
            switch (midle)
            {
                case "==":
                    midle = "=";
                    break;
            }
            string right = String.Join(" ", subs, 2, subs.Length - 1 - 1);

            return DBMapper.getColumName<T>(left) + midle + right;
        }
    }
}
