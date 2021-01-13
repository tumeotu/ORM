using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    interface  ValueStrategy
    {
         object getValue(Expression expr);
    }
}
