using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MyORM.SQLBuilder
{
    internal class ValueOfVariable : IValueOf
    {
        private static ValueOfVariable value = null;
        public static IValueOf clone()
        {
            if (value == null)
            {
                value = new ValueOfVariable();
            }
            return value;
        }
        private ValueOfVariable() { }

        public object getValue(Expression expr)
        {
            MemberExpression member = (MemberExpression)expr;
            ConstantExpression captureConst = (ConstantExpression)member.Expression;
            object value = ((FieldInfo)member.Member).GetValue(captureConst.Value);
            return value;
        }

    }
}
