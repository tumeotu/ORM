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
            this.sql = "Inser Into " + DBMapper.getTablename<T>() + "(" + getColumnName<T>() + ") Values (" + getValue(ob) + ");";
        }
        public string save(T[] arr)
        {
            string valueString = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                valueString += "(" + getValue(arr[i]) + "),";
            }
            valueString += "(" + getValue(arr[arr.Length - 1]) + ")";
            return this.sql = "Inser Into " + DBMapper.getTablename<T>() + "(" + getColumnName<T>() + ") Values " + valueString +";";
        }

        public SqlBuilder<T> AND(Expression<Func<T, bool>> clause)
        {
            this.sql += " AND " + parseClause(clause.Body.ToString());
            return this;
        }

        public SqlBuilder<T> OR(Expression<Func<T, bool>> clause)
        {
            this.sql += " OR " + parseClause(clause.Body.ToString());
            return this;
        }

        public SqlBuilder<T> SelectAll()
        {
            this.sql = "SELECT * FROM " + DBMapper.getTablename<T>()+";";
            return this;
        }

        public SqlBuilder<T> Update(T obj)
        {
            this.sql = "UPDATE " +DBMapper.getTablename<T>() + " SET ";

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
            }
            this.sql += setString.Remove(setString.Length - 1);
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
                    Type myType = typeof(T);
                    PropertyInfo myPropInfo = myType.GetProperty(left);
                    if (myPropInfo.PropertyType == typeof(string))
                    {
                        midle = " LIKE ";
                    }
                    else
                    {
                        midle = "=";
                    }
                    break;
            }
            string right = String.Join(" ", subs, 2, subs.Length - 1 - 1);

            return DBMapper.getColumName<T>(left) + midle + right +";";
        }

        private string getColumnName<T1>()
        {
            string columnNameString = "";
            foreach (PropertyInfo prop in typeof(T1).GetProperties())
            {
                string porpName = prop.Name;
                columnNameString += (DBMapper.getColumName<T>(porpName) + ",");
            }
            return columnNameString.Remove(columnNameString.Length - 1);
        }
        private string getValue(object ob)
        {
            string valueString = "";
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                string porpName = prop.Name;
                var porpValue = prop.GetValue(ob, null);
                if (porpValue != null)
                {
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
            return valueString.Remove(valueString.Length - 1);
        }
    }
}
