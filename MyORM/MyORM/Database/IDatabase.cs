using MyORM.SQLBuilder;
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
		/// Read data using select and where from database
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="queryString">string of query into database</param>
		/// <returns> DataTable of results </returns>
		List<T> Read<T>(string queryString) where T : class, new();


		/// <summary>
		/// Read data using group by and having from database
		/// </summary>
		/// <typeparam name="T, TKey"></typeparam>
		/// <param name="queryString">string of query into database</param>
		/// <returns> DataTable of results </returns>
		Dictionary<TKey, List<T>> ReadGroupBy<TKey,T>(string queryString) where T : class, new();

		/// <summary>
		/// execute query for insert, update, delete
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="queryString"></param>
		bool Execute(string queryString);

		/// <summary>
		/// release object connection
		/// </summary>
		void Dispose();

		SqlBuilder<T> GetQueryBuilder<T>() where T : class, new();
	}

}
