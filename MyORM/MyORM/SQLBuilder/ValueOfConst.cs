using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    class ValueOfConst : ValueStrategy
    {
        private static ValueOfConst value = null;
        public static ValueStrategy clone()
        {
            if (value == null)
            {
                value = new ValueOfConst();
            }
            return value;
        }
        private ValueOfConst() { }
        public object getValue(Expression expr)
        {
            return expr;
        }
    }
}
