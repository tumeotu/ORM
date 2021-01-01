using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
    interface SqlBuilder<T>
    {
        SqlBuilder<T> Where(Expression<Func<T, bool>> clause);
        SqlBuilder<T> AND(Expression<Func<T, bool>> clause);
        SqlBuilder<T> OR(Expression<Func<T, bool>> clause);
        SqlBuilder<T> SelectAll();
        SqlBuilder<T> Update(T obj);
    }
}
