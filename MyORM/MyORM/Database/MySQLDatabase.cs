/*using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Database
{
	class MySQLDatabase : IDatabase
	{
		private string ConnectionString;

		private MySqlConnection Connection;

		private MySqlCommand Command;


		public MySQLDatabase(string connectionString)
		{
			this.ConnectionString = connectionString;
			this.Initlialize();
		}

		public bool Close()
		{
			try
			{
				Connection.Close();
				return true;
			}
			catch( Exception except)
			{
				throw except;
			}
			
		}

		public void Initlialize()
		{
			try
			{
				this.Connection = new MySqlConnection(ConnectionString);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public bool Open()
		{
			try
			{
				Connection.Open();
				return true;
			}
			catch (Exception except)
			{
				throw except;
			}
		}

		public object Read(string queryString)
		{
			DataTable data = new DataTable();
			Connection.Open();
			MySqlDataAdapter da = new MySqlDataAdapter(Command);
			da.Fill(data);
			this.Close();
			da.Dispose();
			return data;
		}

		#region Protected Methods

		/// <summary>
		/// Throws if the connection is null.
		/// </summary>
		/// <exception cref="System.Exception">MySql Connection is not initialized.</exception>
		private void ThrowIfNull()
		{
			if (this.Connection == null)
			{
				//throw new OrmConnectorNotInitializedException();
			}
		}

		#endregion

	}
}
*/