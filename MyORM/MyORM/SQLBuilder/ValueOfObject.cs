using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MyORM.SQLBuilder
{
    class ValueOfObject : IValueOf
    {
        private static ValueOfObject value = null;
        public static IValueOf clone()
        {
            if (value == null)
            {
                value =new ValueOfObject();
            }
            return value;
        }
        private ValueOfObject() { }
        public object getValue(Expression expr)
        {
            MemberExpression member = (MemberExpression)expr;
            MemberExpression captureToProduct = (MemberExpression)member.Expression;
            ConstantExpression captureConst = (ConstantExpression)captureToProduct.Expression;
            object obj = ((FieldInfo)captureToProduct.Member).GetValue(captureConst.Value);
            object value = ((PropertyInfo)member.Member).GetValue(obj, null);
            return value;
        }
    }
}
