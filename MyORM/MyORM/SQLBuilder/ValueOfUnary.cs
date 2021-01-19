using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    class ValueOfUnary : IValueOf
    {

        private static ValueOfUnary value = null;
        public static ValueOfUnary clone()
        {
            if (value == null)
            {
                value = new ValueOfUnary();
            }
            return value;
        }
        private ValueOfUnary() { }

        public object getValue(Expression expr)
        {
            UnaryExpression unary = (UnaryExpression)expr;
            object value = unary.Operand;
            return value;
        }
    }
}
