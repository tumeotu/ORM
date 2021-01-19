using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    class FactoryGetValue
    {
        public static IValueOf getStrategy(Expression expr)
        {
            IValueOf strategy= ValueOfConst.clone();
            
            if (expr is MemberExpression)
            {
                MemberExpression member = (MemberExpression)expr;

                if (member.Expression is MemberExpression) // expr là một property trong object
                {
                    strategy=  ValueOfObject.clone();

                }
                else if (member.Expression is ConstantExpression) // expr là một biến
                {
                    strategy= ValueOfVariable.clone();
                }
                else
                {
                    strategy = ValueOfDatetime.clone();
                }
            }else if (expr is MethodCallExpression)// expr là 1 fuction
            {
                strategy = ValueOfMethod.clone();
            }
            else if (expr is UnaryExpression)
            {
                strategy = ValueOfUnary.clone();
            }
            return strategy;
        }
    }
}
