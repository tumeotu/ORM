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
            this.sql = "Inser Into " + DBMapper.getTablename<T>() + "(" + getColumnName<T>() + ") Values (" + getValue(ob) + ")";
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
            this.sql = "SELECT * FROM " + DBMapper.getTablename<T>();
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
            object rightValue = null;
            string oprearator = "";
            if (right is MemberExpression)
            {
                MemberExpression member = (MemberExpression)right;

                if (member.Expression is MemberExpression) // right là một property trong object
                {
                    MemberExpression captureToProduct = (MemberExpression)member.Expression;
                    ConstantExpression captureConst = (ConstantExpression)captureToProduct.Expression;
                    object obj = ((FieldInfo)captureToProduct.Member).GetValue(captureConst.Value);
                    rightValue = ((PropertyInfo)member.Member).GetValue(obj, null);
                }
                else if (member.Expression is ConstantExpression) // right là một biến
                {
                    ConstantExpression captureConst = (ConstantExpression)member.Expression;
                    rightValue = ((FieldInfo)member.Member).GetValue(captureConst.Value);
                }
            }
            else // right là một biến hằng
            {
                rightValue = right;

            }
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
                        rightValue = String.Format("{0}", rightValue);
                    }
                    else oprearator = "=";
                    break;
                case ExpressionType.NotEqual:

                    if (myPropInfo.PropertyType == typeof(string))
                    {
                        oprearator = "NOT LIKE ";
                        rightValue = String.Format("{0}", rightValue);
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
            return String.Format("{0} {1} {2}", DBMapper.getColumName<T>(leftValue), oprearator, rightValue);
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
            this.sql += String.Format(" Having ({0})", parseClauseHaving(clause.Body));
            return this;
        }

        private static string parseClauseHaving(Expression expr)
        {
            BinaryExpression be = expr as BinaryExpression;
            string ope = checkedExpess(be);
            if (ope != null)
            {
                return String.Format("{0} {1} {2}", parseClauseHaving(be.Left), ope, parseClauseHaving(be.Right));
            }
            return convertToString(be);
        }

        private static string convertToStringHaving(Expression exp)
        {
            BinaryExpression be = exp as BinaryExpression;
            Expression left = be.Left;
            Expression right = be.Right;
            object rightValue = null;
            string oprearator = "";
            if (right is MemberExpression)
            {
                MemberExpression member = (MemberExpression)right;

                if (member.Expression is MemberExpression) // right là một property trong object
                {
                    MemberExpression captureToProduct = (MemberExpression)member.Expression;
                    ConstantExpression captureConst = (ConstantExpression)captureToProduct.Expression;
                    object obj = ((FieldInfo)captureToProduct.Member).GetValue(captureConst.Value);
                    rightValue = ((PropertyInfo)member.Member).GetValue(obj, null);
                }
                else if (member.Expression is ConstantExpression) // right là một biến
                {
                    ConstantExpression captureConst = (ConstantExpression)member.Expression;
                    rightValue = ((FieldInfo)member.Member).GetValue(captureConst.Value);
                }
            }
            else // right là một biến hằng
            {
                rightValue = right;

            }
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
                        rightValue = String.Format("{0}", rightValue);
                    }
                    else oprearator = "=";
                    break;
                case ExpressionType.NotEqual:

                    if (myPropInfo.PropertyType == typeof(string))
                    {
                        oprearator = "NOT LIKE ";
                        rightValue = String.Format("{0}", rightValue);
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
            return String.Format("{0} {1} {2}", DBMapper.getColumName<T>(leftValue), oprearator, rightValue);
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
