using MyORM.Extension;
using MyORM.ORMException;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Database
{
<<<<<<< HEAD
	class PostgreSQLDatabase : IDatabase
	{
		#region Properties
		private string ConnectionString;

		private NpgsqlConnection Connection;

		private NpgsqlCommand Command;

		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="PostgreSQLDatabase"/> class.
		/// </summary>
		/// <param name="connectionString">connectionString to connect to datase, that user wanna/param>
		public PostgreSQLDatabase(Dictionary<string, string> connectionString)
		{
			this.ConnectionString = ConnectionStringConverter.ConvertToPostgreSQL(connectionString);
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
				this.Connection = new NpgsqlConnection(ConnectionString);
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
			NpgsqlDataAdapter da = new NpgsqlDataAdapter(Command);
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
=======
    class PostgreSQLDatabse : IDatabase
    {
        #region Properties
        private string ConnectionString;

        private NpgsqlConnection Connection;

        private NpgsqlCommand Command;

        #endregion

        #region Constructor

        public PostgreSQLDatabase(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Initlialize();
        }

        #endregion

        #region Methods
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

        public void Dispose()
        {
            this.Connection?.Dispose();
            this.Connection = null;
            this.Command = null;
            this.ConnectionString = "";
        }

        public void Initlialize()
        {
            try
            {
                this.Connection = new NpgsqlConnection(ConnectionString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

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

        public object Read(string queryString)
        {
            DataTable data = new DataTable();
            Connection.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(Command);
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
        /// <exception cref="System.Exception">MySql Connection is not initialized.</exception>
        private void ThrowIfNull()
        {
            if (this.Connection == null)
            {
                throw new ConnectorNotInitializedException();
            }
        }

        #endregion
    }
>>>>>>> 85a5d410dd4aa06ecf6f396d75d5c85cac5d3d68
}
