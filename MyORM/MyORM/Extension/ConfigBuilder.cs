using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public class ConfigBuilder : IConfig
	{
		public string Build()
		{
			throw new NotImplementedException();
		}

		public IConfig SetDatabase(string databaseName)
		{
			return null;
		}

		public IConfig SetPassword(string password)
		{
			throw new NotImplementedException();
		}

		public IConfig SetPort(int port)
		{
			throw new NotImplementedException();
		}

		public IConfig SetServer(string serverName)
		{
			throw new NotImplementedException();
		}

		public IConfig SetUserId(string userIDName)
		{
			throw new NotImplementedException();
		}
	}
}
