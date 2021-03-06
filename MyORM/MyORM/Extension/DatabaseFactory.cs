﻿using MyORM.Database;
using MyORM.ORMException;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public static class DatabaseFactory
	{
		/// <summary>
		/// Create DB from Type and config
		/// If Type == None DB will be auto Create from config in func AutoGenerateDatabase
		/// </summary>
		/// <param name="type"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IDatabase GetDatabase(DatabaseType type,Dictionary<string, string> config)
		{
			switch(type)
			{
				case DatabaseType.None:
				{
					return DatabaseFactory.AutoGenerateDatabase(config);
				}
				case DatabaseType.MySQL:
				{
					string connectionString = ConnectionStringConverter.ConvertToMySQL(config);
					return new MySQLDatabase(connectionString);
				}
				case DatabaseType.MSSQL:
				{
					string connectionString = ConnectionStringConverter.ConvertToSQL(config);
					return new SQLDatabase(connectionString);
				}
				case DatabaseType.PostgreSQL:
				{
					string connectionString = ConnectionStringConverter.ConvertToPostgreSQL(config);
					return new PostgreSQLDatabase(connectionString);
				}
				default:
					throw new DatabaseConfigMalformed();
			}	
		}

        /// <summary>
        /// create DB from config
        /// </summary>
        /// <param name="config"></param>
        /// <returns>IDatabase from config from user. The IDatabase is dynamic and dependence with config </returns
        private static IDatabase AutoGenerateDatabase(Dictionary<string, string> config)
        {
            return CheckDatabaseExtention.CheckDatabaseExists(config);
        }
    }
}
