using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.Database.DBHandler
{
	interface IHandlerCheckDB
	{
		/// <summary>
		/// set who can handle request next
		/// </summary>
		/// <param name="handler"></param>
		/// <returns></returns>
		IHandlerCheckDB SetNext(IHandlerCheckDB handler);

		/// <summary>
		/// hanlde the request
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		IDatabase Handle(Dictionary<string, string> config);
	}
}
