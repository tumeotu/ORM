using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Extension
{
	public interface IConfig
	{
		IConfig SetServer(string serverName);
		IConfig SetDatabase(string databaseName);
		IConfig SetUserId(string userIDName);
		IConfig SetPort(int port);
		IConfig SetPassword(string password);

		string Build();
	}
}
