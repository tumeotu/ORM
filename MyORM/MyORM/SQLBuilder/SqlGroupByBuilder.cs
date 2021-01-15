using MyORM.SQLBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORM
{
	/// <summary>
	/// Class override class SqlString to hanlde for group by and having
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	public class SqlGroupByBuilder<T, TKey>: SqlString<T> where T : class, new()
	{
		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="sql"></param>
		public SqlGroupByBuilder(string sql)
		{
			this.sql = sql;
		}

		public override List<T> Get()
		{
			throw new MemberAccessException("The mothod is not support for groupby and having");
		}

		public override Dictionary<TKey, List<T>> GetGroupby<TKey>()
		{
			return this.Database.ReadGroupBy<TKey, T>(this.sql);
		}
	}
}
