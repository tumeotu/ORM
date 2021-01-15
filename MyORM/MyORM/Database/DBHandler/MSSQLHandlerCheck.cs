using MyORM.Extension;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MyORM.Database.DBHandler
{
    class MSSQLHandlerCheck : AbstractHandlerCheckDB
    {
        /// <summary>
        /// Check config is true or not
        /// </summary>
        /// <param name="config"></param>
        /// <returns>MSSQLDatabase if config is true for sql</returns>
        public override IDatabase Handle(Dictionary<string, string> config)
        {
            try
            {
                string connectionString = ConnectionStringConverter.ConvertToSQL(config);
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                connection.Close();
                connection.Dispose();
                return new SQLDatabase(connectionString);
            }
            catch(Exception e)
            {
                return base.Handle(config);
            }
        }
    }
}
