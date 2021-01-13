using MyORM.Database;
using MyORM.Database.DBHandler;
using MyORM.ORMException;
using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MyORM.Extension
{
    static partial class CheckDatabaseExtention
	{
        /// <summary>
        /// Check any DB can valid with config usng chain hanlde from IHandlerCheckDB
        /// </summary>
        /// <param name="config"></param>
        /// <returns>IDatabase from config from user. The IDatabase is dynamic and dependence with config </returns
        public static IDatabase CheckDatabaseExists(Dictionary<string, string> config)
        {
            AbstractHandlerCheckDB MSSQL = new MSSQLHandlerCheck();
            AbstractHandlerCheckDB MySQL = new MySQLHandlerCheck();
            AbstractHandlerCheckDB PostgreSQL = new PostgreSQLHandlerCheck();
            MSSQL.SetNext(MySQL).SetNext(PostgreSQL);
            var result = MSSQL.Handle(config);

            if (result != null)
            {
                return result;
            }
            else
            {
                throw new DatabaseConfigMalformed("Connection string is invalid. Please check again!");
            }
        }        
    }
}
