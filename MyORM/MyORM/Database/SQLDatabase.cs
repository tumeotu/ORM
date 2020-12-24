using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MyORM.ORMException;
using System.Data;
using MyORM.Extension;

namespace MyORM.Database
{
	class SQLDatabase : IDatabase
	{
		#region Properties
		private string ConnectionString;

		private SqlConnection Connection;

		private SqlCommand Command;

		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="SQLDatabase"/> class.
		/// </summary>
		/// <param name="connectionString">connectionString to connect to datase, that user wanna/param>
		public SQLDatabase(Dictionary<string, string> connectionString)
		{
			this.ConnectionString = ConnectionStringConverter.ConvertToSQL(connectionString);
			this.Initlialize(this.ConnectionString);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Close connection to database
		/// </summary>
		public bool Close()
		{
			this.ThrowIfNull();
			try
			{
				Connection.Close();
				return true;
			}
			catch (Exception except)
			{
				throw except;
			}

		}

		/// <summary>
		/// release object connection
		/// </summary>
		public void Dispose()
		{
			this.Connection?.Dispose();
			this.Connection = null;
			this.Command = null;
			this.ConnectionString = "";
		}
		/// <summary>
		/// Initializes the connection against the database.
		/// </summary>
		public void Initlialize(string ConnectionString)
		{
			try
			{
				this.Connection = new SqlConnection(ConnectionString);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
		/// <summary>
		/// Opens the connection to a database.
		/// </summary>
		public bool Open()
		{
			this.ThrowIfNull();
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


		/// <summary>
		/// Read data from database
		/// </summary>
		/// <param name="queryString">string of query into database</param>
		/// <returns> DataTable of results </returns>
		public object Read(string queryString)
		{
			DataTable data = new DataTable();
			Connection.Open();
			SqlDataAdapter da = new SqlDataAdapter(Command);
			da.Fill(data);
			this.Close();
			da.Dispose();
			return data;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Throws if the connection is null.
		/// </summary>
		/// <exception cref="System.Exception">PostgreSQL Connection is not initialized.</exception>
		private void ThrowIfNull()
		{
			if (this.Connection == null)
			{
				throw new ConnectorNotInitializedException();
			}
		}
		#endregion
	}
}
