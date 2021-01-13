using MyORM.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public class ConfigBuilder : IConfig
	{
		#region Properties
		/// <summary>
		/// this is config of connection string
		/// </summary>
		private Dictionary<string, string> ConnectionString = new Dictionary<string, string>();
		/// <summary>
		/// Type of Database is {None, MySQL, MSSQL, PostgreSQL}
		/// </summary>
		private DatabaseType Type = DatabaseType.None;
		#endregion

		#region Method
		/// <summary>
		/// Build the config
		/// </summary>
		/// <param name="type"></param>
		/// <returns>IDatabase is valid with this config</returns>
		public IDatabase Build()
		{
			return DatabaseFactory.GetDatabase(this.Type, this.ConnectionString);
		}

		/// <summary>
		/// set database name 
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IConfig SetDatabase(string databaseName)
		{
			ConnectionString.Add("Database", databaseName);
			return this;
		}

		/// <summary>
		/// set Type of Database is {None, MySQL, MSSQL, PostgreSQL}
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IConfig SetDatabaseType(DatabaseType type)
		{
			this.Type = type;
			return this;
		}

		/// <summary>
		/// set password of account to connect DB
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IConfig SetPassword(string password)
		{
			ConnectionString.Add("Password", password);
			return this;
		}

		/// <summary>
		/// set port of DB
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IConfig SetPort(int port)
		{
			ConnectionString.Add("Port", port.ToString());
			return this;
		}

		/// <summary>
		/// set server name of Database
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IConfig SetServer(string serverName)
		{
			ConnectionString.Add("Server", serverName);
			return this;
		}

		/// <summary>
		/// set username of account to connect DB
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IConfig SetUserId(string userIDName)
		{
			ConnectionString.Add("UserID", userIDName);
			return this;
		}
		#endregion
	}
}
