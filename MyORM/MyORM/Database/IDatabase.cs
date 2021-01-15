using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyORM.Database
{
	public interface IDatabase
	{
		/// <summary>
		/// Opens the connection to a database.
		/// </summary>
		bool Open();
		/// <summary>
		/// Close connection to database
		/// </summary>
		/// <returns></returns>
		bool Close();
		/// <summary>
		/// Initializes the connection against the database.
		/// </summary>
		/// <param name="connectionString">A connection string containing information to connect to a given database.</param>
		void Initlialize(string connectionString);

		/// <summary>
		/// Read data from database
		/// </summary>
		/// <param name="queryString">string of query into database</param>
		/// <returns> DataTable of results </returns>
		object Read(string queryString);

		/// <summary>
		/// release object connection
		/// </summary>
		void Dispose();
	}

}
