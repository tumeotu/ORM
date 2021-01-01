using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database
{
<<<<<<< HEAD
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
=======
    public interface IDatabase
    {
        /// <summary>
        /// Open connection to database
        /// </summary>
        /// <returns></returns>
        bool Open();
        /// <summary>
        /// Close connection to database
        /// </summary>
        /// <returns></returns>
        bool Close();
        /// <summary>
        /// Creater instance of this type
        /// </summary>
        void Initlialize();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString">string of query into database</param>
        /// <returns> DataTable of results </returns>
        object Read(string queryString);
    }
>>>>>>> 85a5d410dd4aa06ecf6f396d75d5c85cac5d3d68
}
