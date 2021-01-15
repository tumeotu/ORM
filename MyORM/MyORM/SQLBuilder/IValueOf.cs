using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    interface  IValueOf
    {
         object getValue(Expression expr);
    }
}
