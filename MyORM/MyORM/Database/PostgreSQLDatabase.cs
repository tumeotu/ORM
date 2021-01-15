using MyORM.Extension;
using MyORM.ORMException;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Database
{
    class PostgreSQLDatabase : IDatabase
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
            this.Initlialize(connectionString);
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

        public void Initlialize(string connectionString)
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

        public DataTable Read(string queryString)
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
}
