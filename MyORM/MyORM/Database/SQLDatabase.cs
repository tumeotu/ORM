using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MyORM.ORMException;
using System.Data;
using MyORM.Extension;
using MyORM.SQLBuilder;
using MyORM.Mapper;

namespace MyORM.Database
{
    class SQLDatabase : IDatabase
    {
        #region Properties
        private string ConnectionString;

        private SqlConnection Connection;

        private SqlCommand Command;

        private DataMapper DataMapper;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLDatabase"/> class.
        /// </summary>
        /// <param name="connectionString">connectionString to connect to datase, that user wanna/param>
        public SQLDatabase(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Initlialize(this.ConnectionString);
            this.DataMapper = new DataMapper();
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

		public SqlBuilder<T> GetQueryBuilder<T>() where T : class, new()
		{
			throw new NotImplementedException();
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

        public void Initlialize()
        {
            throw new NotImplementedException();
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
        public List<T> Read<T>(string queryString) where T : class, new()
        {
            DataTable data = new DataTable();
            Connection.Open();
            Command = new SqlCommand(queryString, Connection);
            SqlDataAdapter da = new SqlDataAdapter(Command);
            da.Fill(data);
            List<T> result = DataMapper.loadAll<T>(data) as List<T>;
            this.Connection.Close();
            return result;
        }


        /// <summary>
        /// Read data using group by and having from database
        /// </summary>
        /// <typeparam name="T, TKey"></typeparam>
        /// <param name="queryString">string of query into database</param>
        /// <returns> DataTable of results </returns>
        public Dictionary<TKey, List<T>> ReadGroupBy<TKey, T>(string queryString) where T : class, new()
        {
            DataTable data = new DataTable();
            Connection.Open();
            Command = new SqlCommand(queryString, Connection);
            SqlDataAdapter da = new SqlDataAdapter(Command);
            da.Fill(data);
            Dictionary<TKey, List<T>> result = DataMapper.loadAll<T>(data) as Dictionary<TKey, List<T>>;
            this.Connection.Close();
            return result;
        }

        /// <summary>
        /// execute query for insert, update, delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryString"></param>
        public bool Execute(string queryString)
        {
            Connection.Open();
            Command = new SqlCommand(queryString, Connection);
            Command.ExecuteNonQuery();
            this.Connection.Close();
            return true;
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
