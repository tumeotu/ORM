using MyORM.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MyORM.SQLBuilder
{
    class SqlString<T> : SqlBuilder<T> where T : class, new()
    {
        public string sql { get; set; }
        private static IDataMapper dataMapper = null;
        static SqlString()
        {
            dataMapper = new DataMapper();
        }
        public void Save<T1>(T ob) where T1 : class, new()
        {
            this.sql = "Insert Into " + dataMapper.GetTablename<T1>() + "(" + getColumnName<T1>() + ") Values (" + getValues<T1>(ob) + ")";
        }
        public string Save<T1>(T[] arr) where T1 : class, new()
        {
            string valueString = "";
            for (int i = 0; i < arr.Length - 1; i++)
            {
                valueString += "(" + getValues<T1>(arr[i]) + "),";
            }
            valueString += "(" + getValues<T1>(arr[arr.Length - 1]) + ")";
            return this.sql = "Inser Into " + dataMapper.GetTablename<T1>() + "(" + getColumnName<T1>() + ") Values " + valueString + ";";
        }

        public SqlBuilder<T> AND(Expression<Func<T, bool>> clause)
        {
            this.sql += String.Format(" AND ({0})", parseClause(clause.Body));
            return this;
        }

        public SqlBuilder<T> OR(Expression<Func<T, bool>> clause) 
        {
            this.sql += String.Format(" OR ({0})", parseClause(clause.Body));
            return this;
        }

        public SqlBuilder<T> SelectAll()
        {
            this.sql = String.Format("SELECT {0} FROM {1} AS {1}", getAllColumnName<T>(), dataMapper.GetTablename<T>());
            return this;
        }
        public SqlBuilder<T> Delete()
        {
            this.sql = "DELETE FROM " + dataMapper.GetTablename<T>();
            return this;
        }

        public SqlBuilder<T> Update(T ob) 
        {
            this.sql = "UPDATE " + dataMapper.GetTablename<T>() + " SET ";// lấy danh sách các thuộc tính của đối tượng
            string setString = "";
            foreach (PropertyInfo prop in ob.GetType().GetProperties())
            {
                string porpName = prop.Name;
                var porpValue = getValueByType(ob, prop);
                string columnName = dataMapper.GetColumName<T>(porpName);
                if (columnName != null && porpValue != null && !dataMapper.IsPrimaryKey<T>(porpName))
                {
                    setString += columnName + "=";
                    if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime))
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
            this.sql += String.Format(" WHERE ({0})", parseClause(clause.Body));
            return this;
        }
        private static string parseClause(Expression expr)
        {
            BinaryExpression be = expr as BinaryExpression;

            string ope = checkedExpess(be);
            if (ope != null)
            {
                return String.Format("{0} {1} {2}", parseClause(be.Left), ope, parseClause(be.Right));
            }
            return convertToString(be);
        }
        private static string checkedExpess(BinaryExpression be)
        {
            switch (be.NodeType)
            {
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.OrElse:
                    return "OR";
            }
            return null;
        }

        private static string convertToString(Expression exp)
        {
            BinaryExpression be = exp as BinaryExpression;
            Expression left = be.Left;
            Expression right = be.Right;
            string oprearator = "";
            ////
            object rightValue = FactoryGetValue.getStrategy(right).getValue(right);
            //Expression left = eq.Left;
            string leftValue = left.ToString().Split('.')[1];
            Type myType = typeof(T);
            PropertyInfo myPropInfo = myType.GetProperty(leftValue);
            switch (be.NodeType)
            {
                case ExpressionType.Equal:

                    if (myPropInfo.PropertyType == typeof(string))
                    {
                        oprearator = " LIKE ";

                    }
                    else oprearator = "=";
                    break;
                case ExpressionType.NotEqual:

                    if (myPropInfo.PropertyType == typeof(string))
                    {
                        oprearator = "NOT LIKE ";
                    }
                    else
                        oprearator = "<>";
                    break;
                case ExpressionType.GreaterThan:
                    oprearator = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    oprearator = ">=";
                    break;
                case ExpressionType.LessThan:
                    oprearator = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    oprearator = "<=";
                    break;
            }
            if (myPropInfo.PropertyType == typeof(string) || myPropInfo.PropertyType == typeof(DateTime))
            {
                rightValue = String.Format("'{0}'", rightValue);
            }
            return String.Format("{0}.{1} {2} {3}", dataMapper.GetTablename<T>(), dataMapper.GetColumName<T>(leftValue), oprearator, rightValue);
        }

        public SqlBuilder<T> GroupBy(Expression<Func<T, object>> clause)
        {
            this.sql += " GROUP BY " + parseClauseGroupBy(clause.Body.ToString());
            return this;
        }
        public SqlBuilder<T> GroupBy(Expression<Func<T, object>> clause1, Expression<Func<T, object>> clause2)
        {
            this.sql += " GROUP BY " + parseClauseGroupBy(clause1.Body.ToString()) + "," + parseClauseGroupBy(clause2.Body.ToString());
            return this;
        }

        private static string parseClauseGroupBy(String clause)
        {
            if (clause.Contains("Convert"))
                clause = clause.Replace("Convert", "");
            char[] separators = new char[] { '(', ')', ' ', };
            string[] subs = clause.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string left = (subs[0].Split('.', (char)StringSplitOptions.RemoveEmptyEntries))[1];
            return DBMapper.getColumName<T>(left);
        }

        public SqlBuilder<T> Having<TKey>(Expression<Func<IGroup<TKey, T>, bool>> clause)
        {
            string data = clause.Body.ToString();
            return this;
        }

        private static string parseClauseHaving(String clause)
        {
            if (clause.Contains("Convert"))
                clause = clause.Replace("Convert", "");
            char[] separators = new char[] { '(', ')', ' ', };
            string[] subs = clause.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            string left = (subs[0].Split('.', (char)StringSplitOptions.RemoveEmptyEntries))[1];
            return DBMapper.getColumName<T>(left);
        }


        private string getAllColumnName<T1>()
        {
            string columnNameString = "";
            string tableName = dataMapper.GetTablename<T1>();
            foreach (PropertyInfo prop in typeof(T1).GetProperties())
            {
                string porpName = prop.Name;
                string coulumnName = dataMapper.GetColumName<T1>(porpName);
                if (coulumnName != null)
                    columnNameString += String.Format("{0}.{1} AS '{0}.{1}',", tableName, coulumnName);
            }
            return columnNameString.Remove(columnNameString.Length - 1);
        }

        private string getColumnName<T1>()
        {
            string columnNameString = "";
            foreach (PropertyInfo prop in typeof(T1).GetProperties())
            {
                string porpName = prop.Name;
                if (!dataMapper.IsPrimaryKey<T1>(porpName) && dataMapper.GetColumName<T1>(porpName) != null)
                {
                    dataMapper.GetColumName<T1>(porpName);
                    columnNameString += (dataMapper.GetColumName<T>(porpName) + ",");
                }
            }
            return columnNameString.Remove(columnNameString.Length - 1);
        }
        private string getValues<T1>(object ob) where T1 : class, new()
        {
            string valueString = "";
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                string porpName = prop.Name;
                if (dataMapper.GetColumName<T1>(porpName) != null)
                {
                    var porpValue = getValueByType(ob, prop);
                    if (porpValue != null)
                    {
                        if (!dataMapper.IsPrimaryKey<T1>(porpName))
                        {

                            if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(DateTime))
                            {
                                valueString += ("'" + porpValue + "'" + ",");
                            }
                            else
                            {
                                valueString += (porpValue.ToString() + ",");
                            }
                        }
                    }
                    else
                    {
                        valueString += "null,";
                    }
                }
            }
            return valueString.Remove(valueString.Length - 1);
        }
        private object getValueByType(object ob, PropertyInfo prop)
        {
            var porpValue = prop.GetValue(ob, null);
            if (prop.PropertyType == typeof(DateTime))
            {
                DateTime date = (DateTime)porpValue;
                porpValue = date.ToString("yyyy/MM/dd HH:mm:ss");
            }
            return porpValue;
        }
    }
}
