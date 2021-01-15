using MyORM.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public interface IConfig
	{
		/// <summary>
		/// set Type of Database is {None, MySQL, MSSQL, PostgreSQL}
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IConfig SetDatabaseType(DatabaseType type);

		/// <summary>
		/// set server name of Database
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IConfig SetServer(string serverName);

		/// <summary>
		/// set database name 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IConfig SetDatabase(string databaseName);

		/// <summary>
		/// set username of account to connect DB
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IConfig SetUserId(string userIDName);

		/// <summary>
		/// set port of DB
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IConfig SetPort(int port);

		/// <summary>
		/// set password of account to connect DB
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IConfig SetPassword(string password);

		/// <summary>
		/// Build the config
		/// </summary>
		/// <param name="type"></param>
		/// <returns>IDatabase is valid with this config</returns>
		IDatabase Build();
	}
}
