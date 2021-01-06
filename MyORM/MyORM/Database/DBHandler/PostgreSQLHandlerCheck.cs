using MyORM.Extension;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database.DBHandler
{
    class PostgreSQLHandlerCheck : AbstractHandlerCheckDB
    {
        /// <summary>
        /// Check config is true or not
        /// </summary>
        /// <param name="config"></param>
        /// <returns>PostgreSQLDatabase if config is true for postgre</returns>
        public override IDatabase Handle(Dictionary<string, string> config)
        {
            try
            {
                string connectionString = ConnectionStringConverter.ConvertToPostgreSQL(config);
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                connection.Close();
                connection.Dispose();
                return new PostgreSQLDatabase(connectionString);
            }
            catch(Exception e)
            {
                return base.Handle(config);
            }
        }
    }

}
