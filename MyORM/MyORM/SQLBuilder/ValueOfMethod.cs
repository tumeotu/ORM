using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    class ValueOfMethod : IValueOf
    {
        private static ValueOfMethod value = null;
        public static ValueOfMethod clone()
        {
            if (value == null)
            {
                value = new ValueOfMethod();
            }
            return value;
        }
        private ValueOfMethod() { }
        public object getValue(Expression expr)
        {
            var objectMember = Expression.Convert(expr, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }
    }
}
