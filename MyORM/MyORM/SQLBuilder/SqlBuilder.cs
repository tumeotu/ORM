using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyORM.SQLBuilder
{
	public interface SqlBuilder<T>
	{
        SqlBuilder<T> Where(Expression<Func<T, bool>> clause);
        SqlBuilder<T> AND(Expression<Func<T, bool>> clause);
        SqlBuilder<T> OR(Expression<Func<T, bool>> clause);
        SqlBuilder<T> SelectAll();
        SqlBuilder<T> Update(T obj);
        SqlBuilder<T> Insert(T obj);
        SqlBuilder<T> Insert(List<T> obj);
        SqlBuilder<T> Delete();
        bool SaveChanges();
        SqlBuilder<T> GroupBy(Expression<Func<T, object>> clause1);
        SqlBuilder<T> Having(Expression<Func<IGroup<T>, bool>> clause);

        List<T> Get();
        Dictionary<TKey, List<T>> GetGroupby<TKey>();
    }
}
