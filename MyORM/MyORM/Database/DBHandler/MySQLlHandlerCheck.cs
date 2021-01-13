using MyORM.Extension;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database.DBHandler
{
    class MySQLHandlerCheck : AbstractHandlerCheckDB
    {
        /// <summary>
        /// Check config is true or not
        /// </summary>
        /// <param name="config"></param>
        /// <returns>MySQLDatabase if config is true for mysql</returns>
        public override IDatabase Handle(Dictionary<string, string> config)
        {
            try
            {
                string connectionString = ConnectionStringConverter.ConvertToMySQL(config);
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                connection.Close();
                connection.Dispose();
                return new MySQLDatabase(connectionString);
            }
            catch(Exception e)
            {
                return base.Handle(config);
            }
        }
    }
}
