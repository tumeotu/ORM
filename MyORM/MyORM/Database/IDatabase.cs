using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database
{
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
		/// 
		/// </summary>
		/// <param name="queryString">string of query into database</param>
		/// <returns> DataTable of results </returns>
		object Read(string queryString);
	}
}
