using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    class ValueOfDatetime : ValueStrategy
    {
        private static ValueOfDatetime value = null;
        public static ValueStrategy clone()
        {
            if (value == null)
            {
                value = new ValueOfDatetime();
            }
            return value;
        }
        private ValueOfDatetime() { }
        public object getValue(Expression expr)
        {
            var objectMember = Expression.Convert(expr, typeof(DateTime));

            var getterLambda = Expression.Lambda<Func<DateTime>>(objectMember);

            var getter = getterLambda.Compile();

            return getter().ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
