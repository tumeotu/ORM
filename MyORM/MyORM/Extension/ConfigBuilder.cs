using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public class ConfigBuilder : IConfig
	{
		private Dictionary<string, string> ConnectionString = new Dictionary<string, string>();
		public Dictionary<string, string> Build()
		{
			return ConnectionString;
		}

		public IConfig SetDatabase(string databaseName)
		{
			ConnectionString.Add("Database", databaseName);
			return this;
		}

		public IConfig SetPassword(string password)
		{
			ConnectionString.Add("Password", password);
			return this;
		}

		public IConfig SetPort(int port)
		{
			ConnectionString.Add("Port", port.ToString());
			return this;
		}

		public IConfig SetServer(string serverName)
		{
			ConnectionString.Add("Server", serverName);
			return this;
		}

		public IConfig SetUserId(string userIDName)
		{
			ConnectionString.Add("UserID", userIDName);
			return this;
		}
	}
}
