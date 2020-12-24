using MyORM.ORMException;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public static class ConnectionStringConverter
	{
		public static string ConvertToMySQL(Dictionary<string, string> connectionString)
		{
			if(!connectionString.ContainsKey("Server")||
				!connectionString.ContainsKey("Database") ||
				!connectionString.ContainsKey("UserID") ||
				!connectionString.ContainsKey("Password"))
			{
				throw new ConnectorNotInitializedException();
			}
			else
			{
				StringBuilder connection =new StringBuilder();
				connection.Append(string.Format("Server={0};", connectionString["Server"]));
				connection.Append(string.Format("Database={0};", connectionString["Database"]));
				connection.Append(string.Format("Uid={0};", connectionString["UserID"]));
				connection.Append(string.Format("Password={0};", connectionString["Password"]));
				return connection.ToString();
			}
		}
		public static string ConvertToSQL(Dictionary<string, string> connectionString)
		{
			if (!connectionString.ContainsKey("Server") ||
				!connectionString.ContainsKey("Database") ||
				!connectionString.ContainsKey("UserID") ||
				!connectionString.ContainsKey("Password"))
			{
				throw new ConnectorNotInitializedException();
			}
			else
			{
				StringBuilder connection = new StringBuilder();
				connection.Append(string.Format("Server={0};", connectionString["Server"]));
				connection.Append(string.Format("Database={0};", connectionString["Database"]));
				connection.Append(string.Format("User Id={0};", connectionString["UserID"]));
				connection.Append(string.Format("Password={0};", connectionString["Password"]));
				return connection.ToString();
			}
		}
		public static string ConvertToPostgreSQL(Dictionary<string, string> connectionString)
		{
			if (!connectionString.ContainsKey("Server") ||
				!connectionString.ContainsKey("Database") ||
				!connectionString.ContainsKey("UserID") ||
				!connectionString.ContainsKey("Port") ||
				!connectionString.ContainsKey("Password"))
			{
				throw new ConnectorNotInitializedException();
			}
			else
			{
				StringBuilder connection = new StringBuilder();
				connection.Append(string.Format("Server={0};", connectionString["Server"]));
				connection.Append(string.Format("Port={0};", connectionString["Port"]));
				connection.Append(string.Format("Database={0};", connectionString["Database"]));
				connection.Append(string.Format("User Id={0};", connectionString["UserID"]));
				connection.Append(string.Format("Password={0};", connectionString["Password"]));
				return connection.ToString();
				return connection.ToString();
			}
		}
	}
}
