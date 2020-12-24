using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace MyORM.Database
{
	class SQLDatabase : IDatabase
	{
		private string ConnectionString;

		private SqlConnection connection;

		private SqlCommand command;

		private SqlDataReader reader;

		public bool Close()
		{
			throw new NotImplementedException();
		}

		public void Initlialize()
		{
			try
			{
				this.connection = new SqlConnection(ConnectionString);
			}
			catch(Exception exception)
			{
				throw exception;
			}
		}

		public bool Open()
		{
			throw new NotImplementedException();
		}

		public object Read(string queryString)
		{
			throw new NotImplementedException();
		}
	}
}
