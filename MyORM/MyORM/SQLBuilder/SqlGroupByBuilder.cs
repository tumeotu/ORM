using MyORM.Database;
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
    public class SqlGroupByBuilder<T, TKey> : SqlString<T> where T : class, new()
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sql"></param>
        public SqlGroupByBuilder(string sql, IDatabase database)
        {
            this.sql = sql;
            Database = database;
        }

        public override List<T> Get()
        {
            return this.Database.Read<T>(this.sql);
        }

        public override Dictionary<TKey, List<T>> GetGroupby<TKey>()
        {
            return this.Database.ReadGroupBy<TKey, T>(this.sql);
        }
    }
}
